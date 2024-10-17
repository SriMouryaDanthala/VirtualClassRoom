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
    public class UserRoleMediator
    {
        private readonly RoleHandler _handler;
        private readonly VirtualClassRoomDbContext _dbContext;
        public UserRoleMediator(VirtualClassRoomDbContext Context)
        {
            _dbContext = Context;
            _handler = new RoleHandler(_dbContext);
        }   

        public ApiResponse<List<UserRoleDTO>> GetAllUserRoles()
        {
            var HandlerResp =  _handler.GetAllRoles();
            List<UserRoleDTO> userRoles = new List<UserRoleDTO>();
            if(HandlerResp.Success && !HandlerResp.Data.Equals(null))
            {
                foreach(var RoleModel in HandlerResp.Data)
                {
                    userRoles.Add(convertToDTO(RoleModel));
                }
            }
            return new ApiResponse<List<UserRoleDTO>>().CreateApiResponse(
                HandlerResp.Success,
                userRoles,
                HandlerResp.failureMessage
            );
        }

        public ApiResponse<UserRoleDTO> GetUserRoleByRoleName(string roleName)
        {
            var handlerResp = _handler.GetRoleByRoleName(roleName);
            UserRoleDTO roleDTO = new UserRoleDTO();
            if (handlerResp.Success)
            {
                roleDTO = convertToDTO(handlerResp.Data);
            }
            return new ApiResponse<UserRoleDTO>().CreateApiResponse(
                handlerResp.Success,
                roleDTO,
                handlerResp.failureMessage
            );
        }

        public ApiResponse<UserRoleDTO> GetUserRoleByRoleId(Guid roleId)
        {
            var handlerResp = _handler.GetRoleByRoleID(roleId);
            UserRoleDTO roleDTO = null;
            if (handlerResp.Success)
            {
                roleDTO = convertToDTO(handlerResp.Data);
            }
            return new ApiResponse<UserRoleDTO>().CreateApiResponse(
                handlerResp.Success,
                roleDTO,
                !handlerResp.Success ? handlerResp.failureMessage : ((roleDTO == null) ? $"No Role Exists with Role Id - {roleId}" : "")
            );
        }

        private UserRoleDTO convertToDTO(UserRoleModel model)
        {
            return new UserRoleDTO
            {
                UserRoleID = model.UserRoleId,
                UserRoleName = model.UserRoleName,
            };
        }
    }
}
