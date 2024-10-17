using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualClassRoomDTO.DTOModels;
using VirtualClassRoomDTO.GenericDataTypes;
using VirtualClassRoomMediator.Mediators;

namespace VirtualClassRoom.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly UserRoleMediator _mediator;
        public UserRolesController(UserRoleMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("GetAllRoles")]
        public ActionResult<ApiResponse<UserRoleDTO>> getAllClassRooms()
        {
            var resp = _mediator.GetAllUserRoles();
            return resp.Success ? Ok(resp) : BadRequest(resp);
        }

        [HttpGet]
        [Route("GetUserRoleByRolID/{roleId}")]
        public ActionResult<ApiResponse<UserRoleDTO>> getUserRoleByRoleId(Guid roleId)
        {
            var resp = _mediator.GetUserRoleByRoleId(roleId);
            return resp.Success ? Ok(resp) : BadRequest(resp);
        }

        [HttpGet]
        [Route("GetUserRoleByRolRoleName/{roleName}")]
        public ActionResult<ApiResponse<UserRoleDTO>> getUserRoleByRoleName(string roleName)
        {
            var resp = _mediator.GetUserRoleByRoleName(roleName);
            return resp.Success ? Ok(resp) : BadRequest(resp);
        }

    }
}
