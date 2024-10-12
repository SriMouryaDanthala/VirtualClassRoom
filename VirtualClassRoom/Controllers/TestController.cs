using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualClassRoomDTO.DTOModels;

namespace VirtualClassRoom.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly VirtualClassRoomMediator.Mediators.UserRoleMediator _mediator;
        public TestController(VirtualClassRoomMediator.Mediators.UserRoleMediator mediator)
        {
            _mediator = mediator;
        }
        [Route("GetAllUserRoles")]
        [HttpGet]
        public ActionResult<List<UserRoleDTO>> GetAllUserRoles()
        {
           return Ok(_mediator.GetAllUserRoles());
        }

    }
}

