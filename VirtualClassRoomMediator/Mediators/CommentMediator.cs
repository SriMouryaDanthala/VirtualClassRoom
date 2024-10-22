using Persistence.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualClassRoomDTO.DTOModels;
using VirtualClassRoomDTO.GenericDataTypes;
using VirtualClassRoomMediator.Handlers;

namespace VirtualClassRoomMediator.Mediators
{
    public class CommentMediator
    {
        private readonly VirtualClassRoomDbContext _dbContext;
        private readonly CommentHandler _handler;
        public CommentMediator(VirtualClassRoomDbContext context)
        {
            _dbContext = context;
            _handler = new CommentHandler(context);
        }

        public ApiResponse<CommentDTO?> AddComment(CommentDTO comment)
        {
            // check weather the user posting the comment and the class room are valid.
            var validUser = new UserMediator(_dbContext).IsExistingUser(comment.CommentUserId);
            var validClassRoom = new ClassRoomMediator(_dbContext).IsVailidClassRoom(comment.CommentClassRoom);
            CommentDTO? updatedComment = null;
            if(validClassRoom && validClassRoom)
            {
                var handlerResp = _handler.InsertComment(comment);
                if (handlerResp.Success)
                {
                     updatedComment = handlerResp.Data;
                }
                return new ApiResponse<CommentDTO?>().CreateApiResponse(handlerResp.Success,
                    updatedComment == null ? comment : updatedComment,
                    handlerResp.failureMessage
                );
            }
            return new ApiResponse<CommentDTO?>().CreateApiResponse(false, comment, "User or Classroom is invalid");
        } 

        public ApiResponse<List<CommentDTO>?> GetCommentsInClassRoom(Guid ClassRoomId, int index)
        {
            // verify weather classroom is a valid classroom.
            var validClassRoom = new ClassRoomMediator(_dbContext).IsVailidClassRoom(ClassRoomId);
            if(validClassRoom)
            {
                var handlerResp = _handler.GetCommentsInClassRoom(ClassRoomId, index);
                return new ApiResponse<List<CommentDTO>?>().CreateApiResponse(
                    handlerResp.Success,
                    handlerResp.Data,
                    handlerResp.failureMessage
                );
            }
            return new ApiResponse<List<CommentDTO>?>().CreateApiResponse(false, null, $"Classroom - {ClassRoomId.ToString()} is not valid");
        }

        public ApiResponse<List<CommentDTO>?> GetCommentsByUser(Guid userId)
        {
            // verify weather user is a valid user
            var validUser = new UserMediator(_dbContext).IsExistingUser(userId);
            if(validUser)
            {
                var handlerResp = _handler.GetCommentsOfUser(userId);
                return new ApiResponse<List<CommentDTO>?>().CreateApiResponse(
                    handlerResp.Success,
                    handlerResp.Data,
                    handlerResp.failureMessage
                );
            }
            return new ApiResponse<List<CommentDTO>?>().CreateApiResponse(false, null, $"user - {userId} is not a valid user");
        }

        public ApiResponse<CommentDTO?> GetCommentByCommentId(Guid commentId)
        {
            var handlerResp = _handler.GetComment(commentId);
            return new ApiResponse<CommentDTO?>().CreateApiResponse(
                handlerResp.Success,
                handlerResp.Data,
                handlerResp.failureMessage
            );
          
        }

        internal bool ValidateComment(Guid commentId)
        {
            return _handler.ValidateComment(commentId);
        }
    }
}
