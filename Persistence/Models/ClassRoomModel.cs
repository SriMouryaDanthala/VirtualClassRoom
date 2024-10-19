using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class ClassRoomModel
    {
        [Key]
        public Guid ClassRoomId { get; set; }
        public string ClassRoomName { get; set; }
        public Guid ClassRoomInchargeId { get; set; }
        [ForeignKey("ClassRoomInchargeId")]
        public UserModel User { get; set; }
        public int ClassRoomCount { get; set; }
        public DateTime ClassRoomTimestamp { get; set; }

        // different classes can have same user
        public ICollection<UserClassRoomJoin> ClassRooms { get; set; }

        // a classroom can have multiple comments
        public ICollection<CommentModel> CommentsInClassRoom { get; set; }

    }
}
