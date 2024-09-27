using Npgsql;
using VirtualClassRoom.DataTypes;

namespace VirtualClassRoom.DataBase
{
    public class UserClassRoomDatabaseManager
    {
        private string _dbManager = new DataBaseManager().getConnectionString();

        public UserClassRoom insertUserClassRoomRelation(string userID, string classRoomID)
        {
            int rowsAffected = 0;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_dbManager))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(createUserClassRoomInsertionString(userID,classRoomID), conn))
                    {
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                if (rowsAffected > 0)
                {
                    return new UserClassRoom
                    {
                        UserID = userID,
                        ClassRoomID = classRoomID,
                        UserClassRoomCreatedAt = DateTime.Now,
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool RemoveUserFromClassRoom(string userID, string classRoomID)
        {
            int rowsAffected = 0;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_dbManager))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(createClassRoomRemovalString(userID, classRoomID), conn))
                    {
                        rowsAffected=cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string createUserClassRoomInsertionString(string userID, string classRoomID)
        {
            return $"INSERT INTO UserClassRoom (User_ID, ClassRoom_ID, UserClassRoom_CreatedAt)" +
                $"SELECT \'{userID}\', \'{classRoomID}\'," +
                $"\'{DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss")}\';";
        }

        private string createClassRoomRemovalString(string userID, string classRoomID)
        {
            return $"DELETE FROM UserClassRoom WHERE user_id = \'{userID}\' AND classroom_id = \'{classRoomID}\'";
        }
    }
}
