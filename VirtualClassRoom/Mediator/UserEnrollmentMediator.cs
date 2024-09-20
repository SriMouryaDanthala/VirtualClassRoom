using VirtualClassRoom.DataBase;
using VirtualClassRoom.DataTypes;
using VirtualClassRoom.DTO;

namespace VirtualClassRoom.Mediator
{
    public class UserEnrollmentMediator
    {
        private ClassRoomDatabaseManager _classroomdbManager = new ClassRoomDatabaseManager();
        private UserDataBaseManager _userdbManager = new UserDataBaseManager(); 
        private UserClassRoomDatabaseManager _userClassRommdbManager = new UserClassRoomDatabaseManager();
        public UserClassRoom enrollUserInClass(UserClassRoom userClassRoom)
        {
            // check if user and class Room  exists 
            UserDTO userDTO = _userdbManager.getUserByUserID(userClassRoom.UserID);
            ClassRoom classRoom = _classroomdbManager.getClassRoomByID(userClassRoom.ClassRoomID);

            if(userDTO!=null && classRoom != null)
            {
                return _userClassRommdbManager.insertUserClassRoomRelation(userClassRoom);
            }
            return null;

        }
    }
}
