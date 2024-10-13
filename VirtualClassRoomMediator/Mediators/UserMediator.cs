using Persistence.DBContext;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualClassRoomDTO.DTOModels;
using VirtualClassRoomDTO.GenericDataTypes;
using VirtualClassRoomMediator.Handlers;

namespace VirtualClassRoomMediator.Mediators
{
    public class UserMediator
    {
        private readonly VirtualClassRoomDbContext _dbContext;
        private readonly UserHandler _handler;
        public UserMediator(VirtualClassRoomDbContext context )
        {
            _dbContext = context;
            _handler = new UserHandler( context );
        }

        public ApiResponse<UserRegistrationDTO> createUser(UserRegistrationDTO newUser)
        {
            var resp = new ApiResponse<UserRegistrationDTO>();
            var roleId = _dbContext.UserRoles.Where(ur => ur.UserRoleName.Equals(newUser.UserRole)).FirstOrDefault()?.UserRoleId;
            HandlerResponse<UserRegistrationDTO> createdUser;
            if (!IsExistingUser(newUser.UserName) && roleId!=null)
            {
                createdUser = _handler.CreateUser(newUser, (Guid)roleId);
                if(createdUser.Success)
                {
                    return resp.CreateApiResponse(
                        true,
                        createdUser.Data
                    );
                }
                else
                {
                    return resp.CreateApiResponse(
                        false,
                        createdUser.Data,
                        resp.failureMessage
                    );
                }
            }
            return resp.CreateApiResponse(
                false,
                null,
                "UserName or Selected Role is Invalid"
            );
        }
        private bool IsExistingUser(string UserName)
        {
            var handlerResp = _handler.GetUserByUserLogin(UserName);
            return handlerResp.Data == null ? false : true;
        }

    }
}
