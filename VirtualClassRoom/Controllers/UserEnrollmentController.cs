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
        public ActionResult<UserClassRoom> enrollUserInClass(UserClassRoom userClassRoom)
        {
            UserClassRoom newUserClassRoom = _mediator.enrollUserInClass(userClassRoom);

            return newUserClassRoom != null ? Ok(newUserClassRoom) : BadRequest(userClassRoom);

        }
    }
}
