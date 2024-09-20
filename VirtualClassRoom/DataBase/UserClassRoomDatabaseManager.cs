using Npgsql;
using VirtualClassRoom.DataTypes;

namespace VirtualClassRoom.DataBase
{
    public class UserClassRoomDatabaseManager
    {
        private string _dbManager = new DataBaseManager().getConnectionString();

        public UserClassRoom insertUserClassRoomRelation(UserClassRoom userClassRoom)
        {
            int rowsAffected = 0;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_dbManager))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(createUserClassRoomInsertionString(ref userClassRoom), conn))
                    {
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                if (rowsAffected > 0)
                {
                    return userClassRoom;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string createUserClassRoomInsertionString(ref UserClassRoom userClassRoom)
        {
            userClassRoom.UserClassRoomCreatedAt = DateTime.Now;
            return $"INSERT INTO UserClassRoom (User_ID, ClassRoom_ID, UserClassRoom_CreatedAt)" +
                $"SELECT \'{userClassRoom.UserID}\', \'{userClassRoom.ClassRoomID}\'," +
                $"\'{userClassRoom.UserClassRoomCreatedAt.ToString("yyyy-MM-dd HH:mm:ss")}\';";
        }
    }
}
