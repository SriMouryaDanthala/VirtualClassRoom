using Microsoft.AspNetCore.Mvc;
using VirtualClassRoom.DataTypes;
using VirtualClassRoom.Mediator;

namespace VirtualClassRoom.Controllers
{
    [ApiController]
    [Route("ClassRoomController")]
    public class ClassRoomController : ControllerBase
    {
        private ClassRoomMediator _classRoomMediator = new ClassRoomMediator();

        [Route("GetAllClassRooms")]
        [HttpGet()]
        public IEnumerable<ClassRoom> getAllClassRooms()
        {
            return _classRoomMediator.getAllClassRooms();
        }

        [Route("GetClassRoomByID/{classRoomID}")]
        [HttpGet()]
        public ActionResult<ClassRoom> getClassRoomByID(string classRoomID)
        {
            ClassRoom room =  _classRoomMediator.getClassRoomByID(classRoomID);
            if (room == null)
            {
                return BadRequest();
            }
            return room;
        }

        [Route("CreateClassRoom")]
        [HttpPost()]
        public ActionResult<ClassRoom> createClassRoom(ClassRoom room) 
        { 
            ClassRoom newClassRoom = _classRoomMediator.createClassRoom(room);
            if (newClassRoom == null)
            {
                return BadRequest();
            }
            return Ok(newClassRoom);    
        }

        [Route("UpateClassRoom")]
        [HttpPut]
        public ActionResult<ClassRoom> postRoom(ClassRoom room)
        {
            ClassRoom updatedClassRoom = _classRoomMediator.UpdateClassRoom(room);
            return updatedClassRoom!=null ? Ok(updatedClassRoom) : BadRequest();
        }

        [Route("DeleteClassRoom/{ClassRoomID}")]
        [HttpDelete]
        public ActionResult deleteClassRoom(string ClassRoomID)
        {
            return _classRoomMediator.DeleteClassRoom(ClassRoomID) ? Ok() : NotFound();
        }

        [Route("getAllStudentsOfClassRoom/{classRoomID}")]
        [HttpGet]
        public ActionResult<string> getStudentsByClassRoomID(string classRoomID)
        {
            return Ok("This is a sample test");
        }
    }

}
