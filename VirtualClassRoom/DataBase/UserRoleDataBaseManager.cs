using Npgsql;
using System.Data;
using VirtualClassRoom.DataTypes;
using VirtualClassRoom.DTO;

namespace VirtualClassRoom.DataBase
{
    public class UserRoleDataBaseManager
    {
        private string _dbConnectionManager = new DataBaseManager().getConnectionString();

        public List<UserRole> getAllUserRoles()
        {
            List<UserRole> roles = new List<UserRole>();
            using (NpgsqlConnection connection = new NpgsqlConnection(this._dbConnectionManager))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(createSelectQueryForAllUserRoles(), connection))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        roles.Add(
                            new UserRole()
                            {
                                UserRoleName = reader["userRole_name"].ToString(),
                                UserRoleID = reader["userRole_ID"].ToString()
                            });
                    }
                }
            }
            return roles;  
        }

        public UserRole getUserRoleByRoleID(string userName)
        {
            UserRole role = null;
            using (NpgsqlConnection connection = new NpgsqlConnection(this._dbConnectionManager))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(createUserRoleByRoleID(userName), connection))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        role = new UserRole()
                        {
                            UserRoleName = reader["userRole_name"].ToString(),
                            UserRoleID = reader["userRole_ID"].ToString()
                        };

                    }
                }
            }
            return role;
        }

        public UserRole getUserRoleByUserName(string userName)
        {
            UserRole role = null;
            using (NpgsqlConnection connection = new NpgsqlConnection(this._dbConnectionManager))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(createUserRoleByUserName(userName), connection))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        role = new UserRole()
                        {
                            UserRoleName = reader["userRole_name"].ToString(),
                            UserRoleID = reader["userRole_ID"].ToString()
                        };

                    }
                }
            }
            return role;
        }

        public UserRole getUserRoleByUserID(string userID)
        {
            UserRole role = null;
            using (NpgsqlConnection connection = new NpgsqlConnection(this._dbConnectionManager))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(createUserRoleByUserID(userID), connection))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        role = new UserRole()
                        {
                            UserRoleName = reader["userRole_name"].ToString(),
                            UserRoleID = reader["userRole_ID"].ToString(),
                            
                        };

                    }
                }
            }
            return role;
        }

        public UserDTO updateUserRole(string userID, string UserRole)
        {
            int rowsAffected = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(this._dbConnectionManager))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(getUserRoleUpdate(userID, UserRole), connection))
                {
                    rowsAffected = cmd.ExecuteNonQuery();
                    if(rowsAffected > 0)
                    {
                        return new UserDataBaseManager().getUserByUserID(userID);
                    }
                }
            }
            return null;
        }

        public string createUserRoleByRoleID(string roleID)
        {
            return $"SELECT * FROM UserRoles WHERE UserRole_ID = \'{roleID}\';";
        }
        public string createSelectQueryForAllUserRoles()
        {
            return "SELECT * FROM USERROLES;";
        }

        public string createSelectQueryForRoleName(string roleName)
        {
            return $"SELECT * FROM USERROLES WHERE userrole_name = \'{roleName}\';";
        }

        public string createInsertQueryForUserRole(UserRole userRole)
        {
            return $"INSERT INTO USERROLE (UserRole_ID, UserRole_Name) " +
                $"SELECT \'{userRole.UserRoleID}\' , \'{userRole.UserRoleName}\';";
        }

        public string createUpdateQueryForUserRole(UserRole role)
        {
            return $"UPDATE USERROLES SET UserRole_Name = \'{role.UserRoleName}\' " +
                $"WHERE UserRole_ID = \'{role.UserRoleID}\'";
        }

        public string createUserRoleByUserName(string userName)
        {
            return "SELECT UserRole_ID,UserRole_name from " +
                $"UserRoles INNER JOIN Users ON " +
                $"UserRoles.UserRole_Id = Users.Users_ID_Role " +
                $"WHERE Users_name = \'{userName}\'";
        }

        public string createUserRoleByUserID(string userID)
        {
            return "SELECT UserRole_ID,UserRole_name from " +
               $"UserRoles INNER JOIN Users ON " +
               $"UserRoles.UserRole_Id = Users.Users_ID_Role " +
               $"WHERE Users_ID = \'{userID}\'";
        }

        public string getUserRoleUpdate(string userId, string roleID)
        {
            return $"UPDATE Users set users_id_Role = \'{roleID}\' WHERE Users_ID = \'{userId}\';";

        }
    }
}
