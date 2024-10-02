using Npgsql;
using System.Runtime.CompilerServices;
using VirtualClassRoom.DataTypes;
using VirtualClassRoom.DTO;

namespace VirtualClassRoom.DataBase
{
    public class UserDataBaseManager
    {
        private string _dataBaseManager = new DataBaseManager().getConnectionString();
        public UserDTO InsertUser(User user)
        {
            int rowsAffected = 0;
            using NpgsqlConnection conn = new NpgsqlConnection(_dataBaseManager);
            
            conn.Open();
            using NpgsqlCommand cmd = new NpgsqlCommand(this.createInsertionStringForUser(ref user), conn);
                
            rowsAffected = cmd.ExecuteNonQuery();
                
            
            if (rowsAffected > 0) {
                // retrun the newly inserted user ID.
                return getUserByUserID(user.UserID);
            }
            return null;
        }

        public UserDTO getUserByUserID(string userID)
        {
            UserDTO newUser = new UserDTO();
            string selectSquel = createSelectStringForUser(userID);
            using (NpgsqlConnection conn = new NpgsqlConnection(_dataBaseManager))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(selectSquel, conn))
                {
                    using NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        newUser.UserID = reader["users_id"].ToString();
                        newUser.UserName = reader["users_name"].ToString();
                        newUser.UserDOB = Convert.ToDateTime(reader["user_dob"].ToString());
                        newUser.UserInsertedAt = Convert.ToDateTime(reader["user_insertedat"].ToString());
                        newUser.userRole = new UserRoleDataBaseManager().getUserRoleByUserID(userID).UserRoleName;
                    }
                }
                conn.Close();
                        
            }
             return newUser.UserID != null ? newUser : null;
        }

        public UserDTO getUserByUserName(string userName)
        {
            UserDTO newUser = new UserDTO();
            using(NpgsqlConnection connection = new NpgsqlConnection(_dataBaseManager))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(createSelectStringForUserByUserName(userName), connection))
                {
                    using NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        newUser.UserID = reader["users_id"].ToString();
                        newUser.UserName = reader["users_name"].ToString();
                        newUser.UserDOB = Convert.ToDateTime(reader["user_dob"].ToString());
                        newUser.UserInsertedAt = Convert.ToDateTime(reader["user_insertedat"].ToString());
                    }
                }
                connection.Close();
            }
            return newUser.UserID !=null ? newUser : null;
        }

        public UserDTO updateUser(User user)
        {
            UserDTO newUser = new UserDTO();
            using (NpgsqlConnection conn = new NpgsqlConnection(_dataBaseManager))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(createUpdateStringForUserUpdation(user), conn))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if(rowsAffected > 0)
                    {
                        newUser = getUserByUserID(user.UserID);
                    }
                }
            }
            return newUser;
        }

        public int deleteUser(string userID)
        {
            int rowsAffected;
            using (NpgsqlConnection conn = new NpgsqlConnection(_dataBaseManager))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(createDeletionStringForUserID(userID), conn))
                {
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

        private string createSelectStringForUser(string userID)
        {
            return $"SELECT * FROM USERS WHERE Users_ID = \'{userID}\'::citext";
        }

        private string createInsertionStringForUser(ref User user)
        {
            user.UserID = Guid.NewGuid().ToString();

            string insertSequel = "INSERT INTO USERS (users_id, users_id_role, users_name, user_dob, user_password, user_insertedAt) SELECT " +
                "\'"+user.UserID + "\', " +
                "\'" + user.UserRole + "\', " +
                "\'" + user.UserName + "\', " +
                "\'" + user.UserDOB.ToString("yyyy-MM-dd HH:mm:ss") + "\' :: DATE, " +
                "\'" + user.UserPassword + "\', " +
                "CURRENT_TIMESTAMP ;";
            return insertSequel;
        } 

        private string createSelectStringForUserByUserName(string userName)
        {
            return $"SELECT users_id " +
            $",userrole_name AS users_id_role " +
            $",users_name " +
            $",user_dob " +
            $",user_insertedat " +
            $"FROM users " +
            $"INNER JOIN userRoles ON users_id_role = userrole_id " +
            $"WHERE users_name = \'{userName}\' ;";
        }

        private string createUpdateStringForUserUpdation(User user)
        {
            return $"UPDATE Users set users_id_role = \'{user.UserRole}\'," +
                $"users_name = \'{user.UserName}\'," +
                $"user_dob = \'{user.UserDOB.ToString("yyyy-MM-dd HH:mm:ss")}\'::DATE," +
                $"user_password = \'{user.Password}\' " +
                $"WHERE USERS_ID = \'{user.UserID}\';";
        }

        private string createDeletionStringForUserID(string userID)
        {
            return $"DELETE FROM Users WHERE Users_ID = \'{userID}\';";
        }


         
    }
}
