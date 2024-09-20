using VirtualClassRoom.DataBase;
using VirtualClassRoom.DataTypes;
using VirtualClassRoom.DTO;

namespace VirtualClassRoom.Mediator
{
    public class UserRoleMediator
    {
        private UserRoleDataBaseManager _userRoleDatabaseManager = new UserRoleDataBaseManager();
        public List<UserRole> getAllUserRoles()
        {
            List<UserRole> roles = _userRoleDatabaseManager.getAllUserRoles();
            return roles;
        }

        public UserRole getUserRoleForUserName(string userName)
        {
            // check weather there is a user with the given userName.
            if( new UserMediator().getUserByUserName(userName) != null)
            {
                return _userRoleDatabaseManager.getUserRoleByUserName(userName);
            }
            return null;
        }

        public UserRole getUserRoleForUserID(string userID)
        {
            if (new UserDataBaseManager().getUserByUserID(userID) != null)
            {
                return _userRoleDatabaseManager.getUserRoleByUserID(userID);
            }
            return null;
        }

        public UserDTO updateUserRole(string userID, string userRole)
        {
            // check weather the user really exists.
            if (new UserDataBaseManager().getUserByUserID(userID) != null)
            {
               return _userRoleDatabaseManager.getUserRoleByRoleID(userRole) != null ? _userRoleDatabaseManager.updateUserRole(userID, userRole) : null;
            }
            return null;
        }
    }
}
