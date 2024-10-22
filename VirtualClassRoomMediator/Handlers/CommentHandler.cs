using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Persistence.DBContext;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VirtualClassRoomDTO.DTOModels;
using VirtualClassRoomDTO.GenericDataTypes;

namespace VirtualClassRoomMediator.Handlers
{
    internal class CommentHandler
    {
        private readonly VirtualClassRoomDbContext _dbContext;
        public CommentHandler(VirtualClassRoomDbContext context)
        {
            _dbContext = context;
        }

        internal HandlerResponse<CommentDTO?> InsertComment(CommentDTO comment)
        {
            try
            {
                CommentModel newComment = new CommentModel()
                {
                    CommentId = Guid.NewGuid(),
                    CommentClassRoom = comment.CommentClassRoom,
                    CommentUserId = comment.CommentUserId,
                    ReplyToComment = (comment.RepliedToComment == null) ? Guid.Empty : comment.RepliedToComment,
                    CommentTimestamp = DateTime.Now.ToUniversalTime(),
                    CommentContent = comment.CommentContent,
                };
                _dbContext.Comments.Add(newComment);
                _dbContext.SaveChanges();
                var updatedComment = this.GetComment(newComment.CommentId);
                return new HandlerResponse<CommentDTO?>().CreateHandlerResponse(true, updatedComment.Data, updatedComment.failureMessage);
            }
            catch (Exception ex)
            {
                return new HandlerResponse<CommentDTO?>().CreateHandlerResponse(
                    false,
                    null,
                    ex.Message
                );
            }
        }

        internal HandlerResponse<CommentDTO?> GetComment(Guid CommentId)
        {
            try
            {
                var result =
                    (
                         from comment in _dbContext.Comments
                         join user in _dbContext.Users
                         on comment.CommentUserId equals user.UserId
                         join classroom in _dbContext.ClassRooms
                         on comment.CommentClassRoom equals classroom.ClassRoomId
                         where comment.CommentId == CommentId
                         select new CommentDTO
                         {
                             CommentClassRoom = classroom.ClassRoomId,
                             CommentUserId = user.UserId,
                             CommentContent = comment.CommentContent,
                             CommentedByUserName = user.UserLogin,
                             RepliedToComment = comment.ReplyToComment,
                             CommentedClassRoomName = classroom.ClassRoomName,
                             CommentedTime = comment.CommentTimestamp,
                             CommentId = comment.CommentId
                         }
                      ).OrderByDescending(c => c.CommentedTime)
                      .FirstOrDefault();
                return new HandlerResponse<CommentDTO?>().CreateHandlerResponse(true, result);
            }
            catch (Exception ex)
            {
                return new HandlerResponse<CommentDTO?>().CreateHandlerResponse(false, null, ex.Message);
            }
        }

        internal HandlerResponse<List<CommentDTO>?> GetCommentsOfUser(Guid userId, int index = 0)
        {
            int low = index <= 0 ? 0 : (index - 1) * DefinedConstants.MAX_ROW_LIMIT;
            try
            {
                var results =
                    (
                         from comment in _dbContext.Comments
                         join user in _dbContext.Users
                         on comment.CommentUserId equals user.UserId
                         join classroom in _dbContext.ClassRooms
                         on comment.CommentClassRoom equals classroom.ClassRoomId
                         where comment.CommentUserId == userId
                         select new CommentDTO
                         {
                             CommentClassRoom = classroom.ClassRoomId,
                             CommentUserId = user.UserId,
                             CommentContent = comment.CommentContent,
                             CommentedByUserName = user.UserLogin,
                             RepliedToComment = comment.ReplyToComment,
                             CommentedClassRoomName = classroom.ClassRoomName,
                             CommentedTime = comment.CommentTimestamp,
                             CommentId = comment.CommentId
                         }
                      ).OrderByDescending(c => c.CommentedTime)
                      .Skip(low)
                      .Take(DefinedConstants.MAX_ROW_LIMIT)
                      .ToList();
                return new HandlerResponse<List<CommentDTO>?>().CreateHandlerResponse(true, results);

            }
            catch (Exception ex)
            {
                return new HandlerResponse<List<CommentDTO>?>().CreateHandlerResponse(true, null,ex.Message);
            }

        }

        internal HandlerResponse<List<CommentDTO>?> GetCommentsInClassRoom(Guid classRoomId, int index = 0)
        {
            int low = index <= 0 ? 0 : (index - 1) * DefinedConstants.MAX_ROW_LIMIT;
            try
            {
                var results =
                    (
                        from comment in _dbContext.Comments
                        join classroom in _dbContext.ClassRooms
                        on comment.CommentClassRoom equals classroom.ClassRoomId
                        join user in _dbContext.Users
                        on comment.CommentUserId equals user.UserId
                        where comment.CommentClassRoom == classRoomId
                        select new CommentDTO()
                        {
                            CommentClassRoom = classroom.ClassRoomId,
                            CommentUserId = user.UserId,
                            CommentContent = comment.CommentContent,
                            CommentedByUserName = user.UserLogin,
                            RepliedToComment = comment.ReplyToComment,
                            CommentedClassRoomName = classroom.ClassRoomName,
                            CommentedTime = comment.CommentTimestamp,
                            CommentId = comment.CommentId
                        }
                    ).OrderByDescending(x => x.CommentedTime)
                    .Skip(low)
                    .Take(DefinedConstants.MAX_ROW_LIMIT)
                    .ToList();
                return new HandlerResponse<List<CommentDTO>?>().CreateHandlerResponse(true, results);
            }
            catch (Exception ex)
            {
                return new HandlerResponse<List<CommentDTO>?>().CreateHandlerResponse(false, null, ex.Message);
            }
        }

        internal bool ValidateComment(Guid commentID)
        {
            try
            {
                var comment = _dbContext.Comments.Where(cmt => cmt.CommentId.Equals(commentID)).FirstOrDefault();
                return !(comment == null);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
