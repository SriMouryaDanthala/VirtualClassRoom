using Microsoft.AspNetCore.Mvc;
using VirtualClassRoomDTO.DTOModels;
using VirtualClassRoomDTO.GenericDataTypes;
using VirtualClassRoomMediator.Mediators;


namespace VirtualClassRoom.Controllers
{
    [ApiController]
    [Route("ClassRoomController")]
    public class ClassRoomController : ControllerBase
    {
       private readonly ClassRoomMediator _mediator;
        public ClassRoomController(ClassRoomMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("NewClassRoom")]
        public ActionResult<ApiResponse<ClassRoomDTO>> createClassRoom(ClassRoomDTO classRoom)
        {
            var res = _mediator.createClassRoom(classRoom);
            return res.Success ? Ok(res) : BadRequest(res);
        }

        [HttpPut]
        [Route("UpdateClassRoom")]
        public ActionResult<ApiResponse<ClassRoomDTO>> updateCLassRoom(ClassRoomDTO  updateClassRoom)
        {
            // I can put a NotFound instead BadRequest.
            var res = _mediator.updateClassRoom(updateClassRoom);
            return res.Success? Ok(res) : BadRequest(res);
        }

        [HttpGet]
        [Route("GetClassRoom/{classRoomID}")]
        public ActionResult<ApiResponse<ClassRoomDTO>> GetClassRoom(Guid classRoomID)
        {
            var res = _mediator.GetClassRoomDetails(classRoomID);
            return res.Success ? Ok(res) : NotFound(res);
        }

        [HttpDelete]
        [Route("RemoveClassRoom/{classRoomId}")]
        public ActionResult<ApiResponse<bool>> RemoveClassRoom(Guid classRoomId)
        {
            var res = _mediator.DeleteClassRoom(classRoomId);
            return res.Success ? Ok(res) : NotFound(res);
        }

        [HttpGet]
        [Route("GetClassRooms")]
        public ActionResult<List<ClassRoomDTO>> GetAllClassRooms()
        {
            var res = _mediator.GetAllClassRooms();
            return res.Success ? Ok(res) : BadRequest(res);
        }
    }

}
