using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VirtualClassRoomDTO.DTOModels;
using VirtualClassRoomDTO.GenericDataTypes;
using VirtualClassRoomMediator.Mediators;


namespace VirtualClassRoom.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserMediator _mediator;
        public UsersController(UserMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("EnrollUser")]
        public ActionResult<ApiResponse<List<UserDTO>>> CreateUser(UserRegistrationDTO newUser)
        {
            var res = _mediator.createUser(newUser);
            return res.Success ? Ok(res) : BadRequest(res);
        }
        [HttpGet]
        [Route("GetUsers")]
        public ActionResult<ApiResponse<List<UserDTO>>> GetUsers()
        {
            var res = _mediator.GetAllUsers();
            return res.Success ? Ok(res) : BadRequest(res);
        }

        [HttpGet]
        [Route("GetUserByUserName/{userName}")]
        public ActionResult<ApiResponse<UserDTO>> GetUserByUserName(string userName)
        {
            var res = _mediator.GetUserByUserName(userName);
            return res.Success ? Ok(res) : NotFound(res);
        }

        [HttpDelete]
        [Route("RemoveUserAccount/{UserID}")]
        public ActionResult<bool> DeleteUser(Guid UserID)
        {
            var res = _mediator.DeleteUser(UserID);
            return res.Success ? Ok(res) : NotFound(res);
        }


    }
}
