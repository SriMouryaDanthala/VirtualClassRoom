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
    // TODO : create a method to retrive user by userID.
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

        public ApiResponse<List<UserDTO>> GetAllUsers()
        {
            var handlerResp = _handler.GetAllUsers();
            List<UserDTO> users = new List<UserDTO>();
            if (handlerResp.Success && handlerResp.Data.Any())
            {
                foreach (var userModel in handlerResp.Data)
                {
                    users.Add(new UserDTO()
                    {
                        UserID = userModel.UserId,
                        UserLogin = userModel.UserLogin,
                    });
                }
                return new ApiResponse<List<UserDTO>>().CreateApiResponse(true, users);
            }
            return new ApiResponse<List<UserDTO>>().CreateApiResponse(false, users, handlerResp.failureMessage);
        }

        public ApiResponse<UserDTO> GetUserByUserName(string UserName)
        {
            HandlerResponse<UserModel> result  = null;
            if(IsExistingUser(UserName))
            {
                result = _handler.GetUserByUserLogin(UserName);
                if(result.Success)
                {
                    return new ApiResponse<UserDTO>().CreateApiResponse(true, new UserDTO()
                    {
                        UserID = result.Data.UserId,
                        UserLogin = result.Data.UserLogin,
                    });
                }
            }
            return new ApiResponse<UserDTO>().CreateApiResponse(false, new UserDTO(),
                !(result == null) ? result.failureMessage : "Not a vaidUser"
            );
        }

        public ApiResponse<bool> DeleteUser(Guid UserId)
        {
            HandlerResponse<bool> User = null;
            if(IsExistingUser(UserId))
            {
                User = _handler.DeleteUser(UserId);
                if(User.Success)
                {
                    return new ApiResponse<bool>().CreateApiResponse(true, User.Data);
                }
            }
            return new ApiResponse<bool>().CreateApiResponse(false, false,
                !(User == null) ? User.failureMessage : "Not a vaidUser"
            );
        }
        private bool IsExistingUser(string UserName)
        {
            var handlerResp = _handler.GetUserByUserLogin(UserName);
            return handlerResp.Data == null ? false : true;
        }
        internal bool IsExistingUser(Guid UserId)
        {
            var handlerResp = _handler.GetUserByUserId(UserId);
            return handlerResp.Data == null ? false : true;
        }

        internal bool IsExistingTeacher(Guid UserId)
        {
            var handlerResp = _handler.GetUserByUserId(UserId);
            var TeacherRole = _dbContext.UserRoles.Where(x => x.UserRoleName.Equals("Teacher")).First().UserRoleId;
            return handlerResp.Data != null && handlerResp.Data.UserRoleId.Equals(TeacherRole) ? true : false;
        }




    }
}
