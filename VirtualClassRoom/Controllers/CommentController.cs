using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualClassRoomDTO.DTOModels;
using VirtualClassRoomDTO.GenericDataTypes;
using VirtualClassRoomMediator.Mediators;

namespace VirtualClassRoom.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentMediator _mediator;
        public CommentController(CommentMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("AddComment")]
        public ActionResult<ApiResponse<CommentDTO?>> AddComment(CommentDTO comment)
        {
            var resp = _mediator.AddComment(comment);
            return resp.Success ? Ok(resp) : BadRequest(resp);
        }

        [HttpGet]
        [Route("CommentsInClassRoom/{classRoomId}")]
        public ActionResult<ApiResponse<List<CommentDTO?>>> GetCommentsInClassRoomn(Guid classRoomId, int index = 0)
        {
            var resp = _mediator.GetCommentsInClassRoom(classRoomId, index);
            return resp.Success ? Ok(resp) : NotFound(resp);
        }

        [HttpGet]
        [Route("CommentsByUser/{UserId}")]
        public ActionResult<ApiResponse<List<CommentDTO?>>> GetCommentsOfUser(Guid UserId)
        {
            var resp = _mediator.GetCommentsByUser(UserId);
            return resp.Success ? Ok(resp) : BadRequest(resp);
        }

        [HttpGet]
        [Route("GetCommentDetial/{commentId}")]
        public ActionResult<ApiResponse<CommentDTO?>> GetCommentDetail(Guid commentId)
        {
            var resp = _mediator.GetCommentByCommentId(commentId);
            return resp.Success ? Ok(resp) : BadRequest(resp);
        }
    }
}
