using Persistence.DBContext;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualClassRoomDTO.GenericDataTypes;

namespace VirtualClassRoomMediator.Handlers
{
    internal class RoleHandler
    {
        public VirtualClassRoomDbContext _dbContext;
        public RoleHandler(VirtualClassRoomDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public HandlerResponse<List<UserRoleModel>> GetAllRoles()
        {
            try
            {
                List<UserRoleModel> UserRoles = _dbContext.UserRoles.ToList();
                return new HandlerResponse<List<UserRoleModel>>().CreateHandlerResponse(
                    true,
                    UserRoles,
                    UserRoles.Any() ? "" : "No Data Found !"
                );
            }
            catch (Exception ex)
            {
                return new HandlerResponse<List<UserRoleModel>>().CreateHandlerResponse(
                    false,
                    new List<UserRoleModel>(),
                    ex.Message
                );
            }
        }

        public HandlerResponse<UserRoleModel> GetRoleByRoleID(Guid roleId)
        {
            try
            {
                var roleModel = _dbContext.UserRoles.Where(role => role.UserRoleId.Equals(roleId)).FirstOrDefault();
                return new HandlerResponse<UserRoleModel>().CreateHandlerResponse(
                    !(roleModel == null),
                    roleModel,
                    (roleModel == null) ? $"No Role Found for Role id -{roleId}" : ""
                );
            }
            catch (Exception ex)
            {
                return new HandlerResponse<UserRoleModel>().CreateHandlerResponse(
                    false,
                    new UserRoleModel(),
                    ex.Message
                );
            }
        }

        public HandlerResponse<UserRoleModel> GetRoleByRoleName(string roleName)
        {
            try
            {
                var roleModel = _dbContext.UserRoles.Where(role => role.UserRoleName.Equals(roleName)).FirstOrDefault();
                return new HandlerResponse<UserRoleModel>().CreateHandlerResponse(
                    !(roleModel == null),
                    roleModel,
                    (roleModel == null) ? $"There is no role  - {roleName}" : ""
                );
            }
            catch (Exception ex)
            {
                return new HandlerResponse<UserRoleModel>().CreateHandlerResponse(
                    false,
                    new UserRoleModel(),
                    ex.Message
                );
            }
        }
    }
}
