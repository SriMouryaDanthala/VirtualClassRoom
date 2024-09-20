using VirtualClassRoom.DataTypes;

namespace VirtualClassRoom.DataTypes
{
    public class User
    {
        public string UserID { get; set; }
        public string UserRole { get; set; }
        public string UserName { get; set; }
        public DateTime UserInsertedAt { get; set; }
        public DateTime UserDOB { get; set; }
        public string Password { get; set; }
        public string UserPassword;
        public User() { }
    }
}

/*User
User_ID
User_Role
User_Name
User_insertedAt
User_DOB
User_password
User_*/

