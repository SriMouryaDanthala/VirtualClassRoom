using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualClassRoomDTO.DTOModels
{
    public class UserRoleDTO
    {
        public Guid UserRoleID { get; set; } = Guid.Empty;
        public string UserRoleName { get; set; } = string.Empty;
    }
}
