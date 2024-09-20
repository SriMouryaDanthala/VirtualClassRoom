using VirtualClassRoom.DataBase;
using VirtualClassRoom.DataTypes;
using VirtualClassRoom.DTO;

namespace VirtualClassRoom.Mediator
{
    public class ClassRoomMediator
    {
        private ClassRoomDatabaseManager _databaseManager = new ClassRoomDatabaseManager();
        public List<ClassRoom> getAllClassRooms()
        {
            return _databaseManager.getAllClassRooms();
        }
        public ClassRoom getClassRoomByID(string classRoomID)
        {
            ClassRoom classRoom = _databaseManager.getClassRoomByID(classRoomID);
            if (classRoom != null && classRoom.ClassRoomID != null)
            {
                return classRoom;
            }
            return null;
        }

        public ClassRoom createClassRoom(ClassRoom room)
        {
            // check weather the class Room incharge is a valid user.
            UserDTO incharge = new UserDataBaseManager().getUserByUserID(room.ClassRoomInchargeID);
            if (incharge != null)
            {
                room.ClassRoomID = Guid.NewGuid().ToString();
                return _databaseManager.InsertClassRoom(room);
            }
            return null;
        }

        public ClassRoom UpdateClassRoom(ClassRoom room)
        {
            // check weather there is an existing classRoom
            ClassRoom existingClassRoom = getClassRoomByID(room.ClassRoomID);
            if (existingClassRoom != null)
            {
                UserDTO incharge = new UserDataBaseManager().getUserByUserID(room.ClassRoomInchargeID);
                if(incharge!=null)
                {
                    ClassRoom updatedClassRoom = new ClassRoom{
                        ClassRoomID = room.ClassRoomID,
                        ClassRoomName = room.ClassRoomName,
                        ClassRoomInchargeID = room.ClassRoomInchargeID,
                        ClassRoomCreatedAt = room.ClassRoomCreatedAt,
                        ClassRoomImageURL = room.ClassRoomImageURL,
                    };
                    return _databaseManager.updateClassRoom(updatedClassRoom);
                }
            }
            return null;
        }

        public Boolean DeleteClassRoom(string classRoomID)
        {
            // check weather there is a classroom with the given ID
            ClassRoom room = getClassRoomByID(classRoomID);
            if (room != null) 
            {
                return _databaseManager.deleteClassRoom(classRoomID);
            }
            return false;
        }
    }
}
