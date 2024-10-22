using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualClassRoomDTO.DTOModels
{
    public class CommentDTO
    {
        
        public Guid? CommentId { get; set; }
        public Guid CommentUserId { get; set; }

        
        public string? CommentedByUserName { get; set; }
        public Guid CommentClassRoom { get; set; }

        
        public string? CommentedClassRoomName { get; set; }

        
        public DateTime? CommentedTime { get; set; }

        public string CommentContent { get; set; }

        
        public Guid? RepliedToComment { get; set; }
    }
}
