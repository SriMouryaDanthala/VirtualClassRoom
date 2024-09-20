using Npgsql;
using System.Globalization;
using System.Security.AccessControl;
using VirtualClassRoom.DataTypes;

namespace VirtualClassRoom.DataBase
{
    public class ClassRoomDatabaseManager
    {
        private string _dataBaseManager = new DataBaseManager().getConnectionString();

        public ClassRoom InsertClassRoom(ClassRoom classRoom)
        {
            int rowsAffected = 0;
            using (NpgsqlConnection conn = new NpgsqlConnection(_dataBaseManager)) {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(createInsertQueryForClassRoom(classRoom), conn))
                {
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            if (rowsAffected > 0)
            {
                return classRoom;
            }
            return null;
        }

        public List<ClassRoom> getAllClassRooms()
        {
            List<ClassRoom> allClassRooms = new List<ClassRoom>();
            using(NpgsqlConnection conn = new NpgsqlConnection(_dataBaseManager))
            {
                conn.Open();
                using(NpgsqlCommand cmd = new NpgsqlCommand(createSelectStringForAllUsers(), conn))
                {
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ClassRoom newClassRoom = new ClassRoom();
                        newClassRoom.ClassRoomID = reader["ClassRoom_ID"].ToString();
                        newClassRoom.ClassRoomName = reader["ClassRoom_name"].ToString();
                        newClassRoom.ClassRoomInchargeID = reader["ClassRoomIncharge_ID"].ToString();
                        newClassRoom.ClassRoomCreatedAt = Convert.ToDateTime(reader["ClassRoomCreatedAt"].ToString());
                        newClassRoom.ClassRoomImageURL = reader["ClassRoomImageURL"].ToString();
                        allClassRooms.Add(newClassRoom);
                    }
                }
            }
            return allClassRooms;
        }

        public ClassRoom getClassRoomByID(String classRoomID)
        {
            ClassRoom newClassRoom = new ClassRoom();
            using (NpgsqlConnection conn = new NpgsqlConnection(_dataBaseManager))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(createSelectStringForClassRoomID(classRoomID), conn))
                {
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        newClassRoom.ClassRoomID = reader["ClassRoom_ID"].ToString();
                        newClassRoom.ClassRoomName = reader["ClassRoom_name"].ToString();
                        newClassRoom.ClassRoomInchargeID = reader["ClassRoomIncharge_ID"].ToString();
                        newClassRoom.ClassRoomCreatedAt = Convert.ToDateTime(reader["ClassRoomCreatedAt"].ToString());
                        newClassRoom.ClassRoomImageURL = reader["ClassRoomImageURL"].ToString();
                    }
                }
            }
            if(newClassRoom.ClassRoomID!=null) return newClassRoom;

            return null;
        }

        public ClassRoom updateClassRoom(ClassRoom classRoom)
        {
            ClassRoom updatedClassRoom = null;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_dataBaseManager))
                {
                    connection.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(createUpdateStringForClassRoomUpdation(classRoom), connection))
                    {
                        int affectedRows = cmd.ExecuteNonQuery();
                        if (affectedRows > 0)
                        {
                            updatedClassRoom = getClassRoomByID(classRoom.ClassRoomID);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return updatedClassRoom;
        }

        public Boolean deleteClassRoom(string classRoomID)
        {
            int affectedRows = 0;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_dataBaseManager))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(createDeletionStringForClassRoom(classRoomID), conn))
                    {
                        affectedRows = cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return affectedRows > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string createSelectStringForAllUsers()
        {
            return $"SELECT * FROM ClassRoom;";
        }

        private string createSelectStringForClassRoomID(String userId)
        {
            return $"SELECT * FROM ClassRoom where ClassRoom_ID = \'{userId}\';";
        }

        private string createInsertQueryForClassRoom(ClassRoom newClassRoom)
        {
            return $"INSERT INTO ClassRoom (ClassRoom_ID,ClassRoom_name,ClassRoomIncharge_ID,ClassRoomImageURL,ClassRoomCreatedAt) " +
                $"SELECT \'{newClassRoom.ClassRoomID}\'," +
                $"\'{newClassRoom.ClassRoomName}\'," +
                $"\'{newClassRoom.ClassRoomInchargeID}\'," +
                $"\'{newClassRoom.ClassRoomImageURL}\'," +
                $"\'{newClassRoom.ClassRoomCreatedAt.ToString("yyyy-MM-dd HH:mm:ss")}\';";
        }

        private string createUpdateStringForClassRoomUpdation(ClassRoom ClassRoom)
        {
            return $"UPDATE ClassRoom set Classroom_name = \'{ClassRoom.ClassRoomName}\'," +
                $"classroomincharge_id = \'{ClassRoom.ClassRoomInchargeID}\'," +
                $"classroomimageurl = \'{ClassRoom.ClassRoomImageURL}\' " +
                $"WHERE Classroom_id = \'{ClassRoom.ClassRoomID}\';";
        }

        private string createDeletionStringForClassRoom(string classRoomID)
        {
            return $"DELETE FROM ClassRoom where ClassRoom_id = \'{classRoomID}\';";
        }
    }
}
