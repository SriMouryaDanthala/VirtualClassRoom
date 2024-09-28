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
        private UserMediator _userMediator = new UserMediator();    

        public UserClassRoom enrollUserInClass(string userID, string classRoomID)
        {
            // check if user and class Room  exists 
            UserDTO userDTO = _userdbManager.getUserByUserID(userID);
            ClassRoom classRoom = _classroomdbManager.getClassRoomByID(classRoomID);

            if(userDTO!=null && classRoom != null)
            {
                return _userClassRommdbManager.insertUserClassRoomRelation(userID, classRoomID);
            }
            return null;
        }

        public bool RemoveUserFromClassRoom(string userID, string classRoomID)
        {
            // check if the user and classroom are valid and the user is not already enrolled.
            if (_userdbManager.getUserByUserID(userID) != null && _classroomdbManager.getClassRoomByID(classRoomID) != null)
            {
                return _userClassRommdbManager.RemoveUserFromClassRoom(userID, classRoomID);
            }
            return false;
        }

        public List<ClassRoomDTO> GetClassRoomsEnrolledByUser(string userID)
        {
            if(_userMediator.IsAValidUser(userID))
                return _userClassRommdbManager.getAllClassRoomsEnrolledByUser(userID).OrderBy(x=>x.ClassRoomName).ToList();
            return new List<ClassRoomDTO>();
        }

    }
}
