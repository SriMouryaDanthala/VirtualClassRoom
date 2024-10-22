using Persistence.DBContext;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualClassRoomDTO.DTOModels;
using VirtualClassRoomDTO.GenericDataTypes;
using VirtualClassRoomMediator.Utilities;

namespace VirtualClassRoomMediator.Handlers
{
    internal class UserHandler
    {
        private readonly VirtualClassRoomDbContext _dbContext;
        public UserHandler(VirtualClassRoomDbContext context)
        {
            _dbContext = context;
        }


        public HandlerResponse<UserDTO> CreateUser(UserRegistrationDTO user,Guid UserRoleID)
        {
            // write an equivalent of the insert SQL.
            var response = new HandlerResponse<UserDTO>(); 
            UserModel newUser =  new UserModel()
            {
                UserId = Guid.NewGuid(),
                UserLogin = user.UserName,
                UserPassword = new HashingUtility().HashTheText(user.UserPassword),
                UserRoleId = UserRoleID,
                UserTimestamp = DateTime.Now.ToUniversalTime(),
            };
            try
            {
                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();
                return response.CreateHandlerResponse(
                        true,
                        new UserDTO()
                        {
                            UserID = newUser.UserId,
                            UserLogin = newUser.UserLogin
                        }
                );

            }
            catch (Exception ex)
            {
                return response.CreateHandlerResponse(false, null, ex.Message);
            }
        }

        public HandlerResponse<UserModel> GetUserByUserId(Guid UserId)
        {
            var response = new HandlerResponse<UserModel>();
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.UserId.Equals(UserId));
                return response.CreateHandlerResponse(true, user);
            }
            catch (Exception ex)
            {
                return response.CreateHandlerResponse(false, null, ex.Message);
            }
        }

        public HandlerResponse<UserModel> GetUserByUserLogin(string UserLogin)
        {
            var response = new HandlerResponse<UserModel>();
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.UserLogin.Equals(UserLogin));
                return response.CreateHandlerResponse(true, user);
            }
            catch (Exception ex)
            {
                return response.CreateHandlerResponse(false, null, ex.Message);
            }
        }

        public HandlerResponse<List<UserModel>> GetAllUsers()
        {
            var result = new HandlerResponse<List<UserModel>>();
            try
            {
                var users = _dbContext.Users.ToList();
                return result.CreateHandlerResponse(true, users);
            }
            catch (Exception ex)
            {
                return result.CreateHandlerResponse(false, null, ex.Message);
            }
        }

        public HandlerResponse<bool> DeleteUser(Guid UserId)
        {
            var response = new HandlerResponse<bool>();
            try
            {
                var user = _dbContext.Users.Where(u => u.UserId.Equals(UserId)).First();
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                return response.CreateHandlerResponse(true, true);

            }
            catch (Exception ex)
            {
                return response.CreateHandlerResponse(false, false, ex.Message);
            }
        }

        public HandlerResponse<UserDTO> UpdateUserLogin(UserDTO user)
        {
            var response =  new HandlerResponse<UserDTO>();
            try
            {

                var OldUser = _dbContext.Users.Find(user.UserID);
                OldUser.UserLogin = user.UserLogin;
                _dbContext.SaveChanges();
                return response.CreateHandlerResponse(true, user);
            }
            catch (Exception ex)
            {
                return response.CreateHandlerResponse(false, user, ex.Message);
            }
        }
            
        

    }
}
