using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualClassRoomDTO.GenericDataTypes;
using VirtualClassRoomMediator.Mediators;

namespace VirtualClassRoom.Controllers
{
    [Route("LoginController")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginMediator _mediator;
        public LoginController(LoginMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<ApiResponse<JwtResponse>> Login(string userName, string password)
        {
            var resp = _mediator.HandlerUserLogin(userName, password);
            return resp.Success ? Ok(resp.Data) : BadRequest(resp.Data);
        }
    }
}
