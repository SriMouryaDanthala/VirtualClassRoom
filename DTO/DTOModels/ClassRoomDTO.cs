using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualClassRoomDTO.DTOModels
{
    public class ClassRoomDTO
    {
        public Guid ClassRoomId { get; set; } = Guid.Empty;
        public string ClassRoomName { get; set; }
        public Guid ClassRoomInchargeId { get; set; }
        public int? ClassRoomCount { get; set; }
    }
}
