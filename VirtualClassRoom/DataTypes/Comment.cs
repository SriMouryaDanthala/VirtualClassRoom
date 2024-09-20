using VirtualClassRoom.DataTypes;

namespace VirtualClassRoom.DataTypes
{
    public class Comment
    {
        public string CommentID { get; set; }
        public string CommentContent { get; set; }
        public string CommentUserID { get; set; }
        public string CommentLectureID { get; set; }
        public string CommentSuperCommentID { get; set; }
        public DateTime CommentCreatedTime { get; set; }
    }
}

/*Comment
Comment_ID
Comment_Content
Comment_UserID
Comment_Lecture
Comment_Time
Comment_SuperComment_ID*/
