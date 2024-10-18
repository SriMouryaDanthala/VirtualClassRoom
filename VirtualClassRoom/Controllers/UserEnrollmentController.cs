using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualClassRoomDTO.DTOModels;
using VirtualClassRoomDTO.GenericDataTypes;
using VirtualClassRoomMediator.Mediators;

namespace VirtualClassRoom.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserEnrollmentController : ControllerBase
    {
        private readonly UserClassRoomMediator _mediator;
        public UserEnrollmentController(UserClassRoomMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetUserClassRooms")]
        public ActionResult<ApiResponse<List<UserClassRoomDTO>>> GetUserClassRooms(Guid userId)
        {
            var resp = _mediator.GetAllClassRoomsEnrolledByUser(userId);
            return resp.Success ? Ok(resp) : BadRequest(resp);
        }

        [HttpPost]
        [Route("EnrollUserInClassRoom")]
        public ActionResult<ApiResponse<UserClassRoomSimplifiedDTO>> EnrollUserInClassRoom(Guid userId, Guid classRoomId)
        {
            var resp = _mediator.EnrollUserInClassRoom(userId, classRoomId);
            return resp.Success ? Ok(resp) : BadRequest(resp);
        }

        [HttpGet]
        [Route("GetStudentsInClassRoom")]
        public ActionResult<ApiResponse<List<UserDTO>>> GetStudentsInClassRoom(Guid classRoomId)
        {
            var resp = _mediator.GetAllUsersEnrolledInClassRoom(classRoomId);
            return resp.Success ? Ok(resp) : BadRequest(resp); 
        }

        [HttpDelete]
        [Route("DeEnrollUser")]
        public ActionResult<ApiResponse<bool>> DeEnrollUser(Guid userId, Guid classRoomId)
        {
            var resp = _mediator.RemoveUserFromClassRoom(userId, classRoomId);
            return resp.Success ? Ok(resp) : BadRequest(resp);
        }
    }
}
