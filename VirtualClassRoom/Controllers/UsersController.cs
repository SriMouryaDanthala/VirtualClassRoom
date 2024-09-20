using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VirtualClassRoom.DataBase;
using VirtualClassRoom.DataTypes;
using VirtualClassRoom.DTO;
using VirtualClassRoom.Mediator;


namespace VirtualClassRoom.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserMediator _mediator = new UserMediator();
        
        [Route("CreateUser")]
        [HttpPost]
        public ActionResult<UserDTO> createUser(User user)
        {
            return Ok(_mediator.createUser(user));
        }

        [Route("GetUserByUserName/{userName}")]
        [HttpGet]
        public ActionResult<UserDTO> GetUser(string userName)
        {
            UserDTO user = _mediator.getUserByUserName(userName);
            return user!=null ? Ok(user): NotFound();
        }

        [Route("UpdateUser")]
        [HttpPut]
        public ActionResult<UserDTO> UpdateUser(User user)
        {
            UserDTO updatedUser = _mediator.UpdateUser(user);
            if (updatedUser != null)
            {
                return Ok(updatedUser);
            }
            return BadRequest(user);
        }

        [Route("DeleteUser/{userID}")]
        [HttpDelete]
        public ActionResult DeleteUser(string userID)
        {
            if (_mediator.DeleteUser(userID) > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
