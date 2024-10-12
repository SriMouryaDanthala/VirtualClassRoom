using Persistence.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualClassRoomDTO.DTOModels;

namespace VirtualClassRoomMediator.Mediators
{
    public class UserRoleMediator
    {
        private readonly UserRoleDbContext _userRoleDbContext;
        public UserRoleMediator(UserRoleDbContext userRoleDbContext)
        {
            _userRoleDbContext = userRoleDbContext;
        }   

        public List<UserRoleDTO> GetAllUserRoles()
        {
            return _userRoleDbContext.GetAllRoles();
        }
    }
}
