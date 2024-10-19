using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class CommentModel
    {
        [Key]
        public Guid CommentId { get; set; }
        public Guid CommentUserId { get; set; }
        [ForeignKey("CommentUserId")]
        public UserModel CommentedByUser { get; set; }
        public Guid CommentClassRoom { get; set; }
        [ForeignKey("CommentClassRoom")]
        public ClassRoomModel ClassRoomOfTheComment { get; set; }

        public string CommentContent { get; set; }
        public Guid CommentType { get; set; }
        public Guid? ReplyToComment { get; set; }
        public DateTime CommentTimestamp { get; set; }
    }
    /*
     * The relation between user and comment is 1 to Many, since one user can make many comments.
     * The relation between ClassRoom and comment is 1 to Many, since one ClassRoom can have many comments.
     */
}
