using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualClassRoom.DataTypes;
using VirtualClassRoom.Mediator;

namespace VirtualClassRoom.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserEnrollmentController : ControllerBase
    {
        private UserEnrollmentMediator _mediator = new UserEnrollmentMediator();
        [Route("EnrollUserInClassRoom")]
        [HttpPost]
        public ActionResult<UserClassRoom> enrollUserInClass(string userID, string classRoomID)
        {
            UserClassRoom newUserClassRoom = _mediator.enrollUserInClass(userID,classRoomID);
            return newUserClassRoom != null ? Ok(newUserClassRoom) : BadRequest();
        }

        [Route("DeleteUserForm")]
        [HttpDelete]
        public ActionResult<bool> DeleteUserFormClassRoom(string userID, string classRoomID)
        {
            return _mediator.RemoveUserFromClassRoom(userID, classRoomID)? Ok(true) : BadRequest(false);
        }

    }
}
