using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.DBContext;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VirtualClassRoomMediator.Handlers;
using VirtualClassRoomMediator.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using VirtualClassRoomDTO.GenericDataTypes;
using System.Text.Json.Nodes;

namespace VirtualClassRoomMediator.Mediators
{
    public class LoginMediator
    {
        private readonly VirtualClassRoomDbContext _dbcontext;
        private readonly IConfiguration _configuration;
        public LoginMediator(VirtualClassRoomDbContext dbContext, IConfiguration configuration)
        {
            _dbcontext = dbContext;
            _configuration = configuration;
        }
        public ApiResponse<JwtResponse> HandlerUserLogin(string username, string password)
        {
            var _userMediator = new UserMediator(_dbcontext);
            var validUser = _userMediator.IsExistingUser(username);
            if(validUser)
            {
                // by passing the flow, calling user handler here and checking for the credentials.
                var user = new UserHandler(_dbcontext).GetUserByUserLogin(username);
                if (user.Success)
                {
                    var hashedPass = new HashingUtility().HashTheText(password);
                    if(user.Data.UserPassword.Equals(hashedPass))
                    {
                        /*
                         * the user is a valid user and the authentication has been suceeded.
                         * now generate a token and retrun to authenticate.
                         */
                        var JwtToken = GenerateJwtToken(user.Data);
                        return new ApiResponse<JwtResponse>().CreateApiResponse(true,
                            new JwtResponse() {JwtToken = JwtToken}, "");
                    }
                }
            }
            return new ApiResponse<JwtResponse>().CreateApiResponse(false, new JwtResponse(), "UserName or password is invalid");
        }

        private string GenerateJwtToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: new[]
                {
            new Claim(ClaimTypes.Name, user.UserLogin),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                },
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
