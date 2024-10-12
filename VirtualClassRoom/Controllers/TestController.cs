using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualClassRoomDTO.DTOModels;
using VirtualClassRoomMediator.Mediators;

namespace VirtualClassRoom.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private UserRoleMediator _mediator;
        public TestController(UserRoleMediator mediator)
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

