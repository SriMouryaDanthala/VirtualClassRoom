using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class UserModel
    {
        [Key]
        public Guid UserId { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }

        public Guid UserRoleId { get; set; }
        [ForeignKey("UserRoleId")]
        public UserRoleModel UserRole { get; set; }
        public DateTime UserTimestamp { get; set; }

        // a user can be incharge to many classrooms.
        public ICollection<ClassRoomModel> ClassRooms { get; set; }
        // a user can be enrolled in many classes.
        public ICollection<UserClassRoomJoin> UserClassRoomJoins { get; set; }
    }
}
