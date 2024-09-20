using Npgsql;
using System.Text;
using VirtualClassRoom.DataBase;
using VirtualClassRoom.DataTypes;
using System.Security.Cryptography;
using VirtualClassRoom.DTO;
using Microsoft.Extensions.Logging.Abstractions;

namespace VirtualClassRoom.Mediator
{
    public class UserMediator
    {

        public UserDTO createUser(User user)
        {
            user.UserPassword = this.hashUserPassword(user.Password);
            return InsertUser(user);
        }

        public UserDTO getUserByUserName(string userName) 
        {
            return new UserDataBaseManager().getUserByUserName(userName);
        }

        public UserDTO UpdateUser(User user)
        {
            // Check Weather the user exists 
            UserDataBaseManager dataBaseManager = new UserDataBaseManager();
            UserDTO existingUser = dataBaseManager.getUserByUserID(user.UserID);
            if (existingUser.UserID !=null)
            {
                // hash the latest password and then update the user record.
                user.Password = hashUserPassword(user.Password);
                return dataBaseManager.updateUser(user);
            }
            return null;
        }

        public int DeleteUser(string userID)
        { 
            int rows = new UserDataBaseManager().deleteUser(userID);
            return rows;
        }

        private string hashUserPassword(string password)
        {
            return  Convert.ToBase64String(HashPassword(password));
        }

        private UserDTO  InsertUser(User user)
        {
            return new UserDataBaseManager().InsertUser(user);
        }

        private byte[] HashPassword(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            return provider.ComputeHash(encoding.GetBytes(password));
        }
    }
}
