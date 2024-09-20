using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualClassRoom.DataTypes;
using VirtualClassRoom.DTO;
using VirtualClassRoom.Mediator;

namespace VirtualClassRoom.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private UserRoleMediator _mediator = new UserRoleMediator();
        [Route("GetAllUserRoles")]
        [HttpGet]
        public ActionResult<List<UserRole>> GetAllUserRoles()
        {
            List<UserRole> roles = _mediator.getAllUserRoles();
            if (roles.Any()) return Ok(roles);
            return NotFound();

        }

        [Route("GetUserRoleByUserName/{userName}")]
        [HttpGet]
        public ActionResult<UserRole> getUserRoleForUserName(string userName)
        {
            UserRole role = _mediator.getUserRoleForUserName(userName);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [Route("GetUserRoleByUserID/{userID}")]
        [HttpGet]
        public ActionResult<UserRole> getUserRoleByUserID(string userID)
        {
            UserRole role = _mediator.getUserRoleForUserID(userID);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [Route("UpdateUserRole")]
        [HttpPut]
        public ActionResult<UserDTO> updateUserRole(string userID, string roleID)
        {
            UserDTO updatedUser = _mediator.updateUserRole(userID, roleID);
            if(updatedUser == null) return NotFound();
            return Ok(updatedUser);
        }
    }
}
