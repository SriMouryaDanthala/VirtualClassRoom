using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualClassRoomDTO.DTOModels;

namespace Persistence.DBContext
{
    public class UserRoleDbContext
    { 

        private readonly VirtualClassRoomDbContext _appDbContext;
        public UserRoleDbContext(VirtualClassRoomDbContext context)
        {
            _appDbContext = context;    
        }

        public List<UserRoleDTO> GetAllRoles()
        {
            List<UserRoleDTO> userRoles = new List<UserRoleDTO>();
            foreach(UserRoleModel userRoleModel in _appDbContext.UserRoles.ToList())
            {
                userRoles.Add(new UserRoleDTO()
                {
                    UserRoleID = userRoleModel.UserRoleId,
                    UserRoleName = userRoleModel.UserRoleName

                });
            }
            return userRoles;
        }


        
    }
}
