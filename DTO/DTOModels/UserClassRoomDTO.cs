using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualClassRoomDTO.DTOModels
{
    public class UserClassRoomDTO
    {
        public Guid ClassRoomId { get; set; } = Guid.Empty;
        public Guid EnrolledUserId { get; set; }
        public string classRoomName { get; set; }
        public Guid classRoomInchargeID { get; set; }
        public string classRoomInchargeLogin { get; set; }
    }
}
