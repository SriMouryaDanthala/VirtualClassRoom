using Persistence.DBContext;
using Persistence.Models;
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
    public class UserClassRoomMediator
    {
        private readonly VirtualClassRoomDbContext _dbContext;
        private readonly UserClassRoomHandler _handler;
        private readonly UserHandler _userHandler;
        private readonly ClassRoomMediator _classRoomMediator;
        private readonly UserMediator _userMediator;
        public UserClassRoomMediator(VirtualClassRoomDbContext context)
        {
            _dbContext = context;
            _handler = new UserClassRoomHandler(context);
            _userHandler = new UserHandler(context);
            _classRoomMediator = new ClassRoomMediator(context);
            _userMediator = new UserMediator(context);
        }

        public ApiResponse<UserClassRoomSimplifiedDTO> EnrollUserInClassRoom(Guid userId, Guid classRoomId)
        {
            // verify weather the user and the classrooms are vaild.
            bool validUser = _userMediator.IsExistingUser(userId);
            bool validClassRoom = _classRoomMediator.IsVailidClassRoom(classRoomId);
            bool handlerSuccess = true;
            HandlerResponse<UserClassRoomJoin> handlerResp = new HandlerResponse<UserClassRoomJoin>();
            if (validUser && validClassRoom)
            {
                handlerResp = _handler.InsertUserClassRoom(userId, classRoomId);
                handlerSuccess = handlerResp.Success;
                if(handlerSuccess)
                {
                    return new ApiResponse<UserClassRoomSimplifiedDTO>().CreateApiResponse(
                        true,
                        new UserClassRoomSimplifiedDTO()
                        {
                           UserId = handlerResp.Data.UserId,
                           ClassRoomId = handlerResp.Data.ClassRoomId
                        }
                    );
                }
            }
            return new ApiResponse<UserClassRoomSimplifiedDTO>().CreateApiResponse(
                false,
                new UserClassRoomSimplifiedDTO() { UserId = userId, ClassRoomId = classRoomId },
                (!validUser || !validClassRoom) ? "Not A vaild user or classRoom" : (!handlerSuccess ? handlerResp.failureMessage : "Unknown failure")
            );
        }

        public ApiResponse<List<UserClassRoomDTO>> GetAllClassRoomsEnrolledByUser(Guid UserId)
        {
            if (new UserMediator(_dbContext).IsExistingUser(UserId))
            {
                var handlerResp = _handler.GetAllClassRoomsEnrolledByUser(UserId);
                List<UserClassRoomDTO> enrolledClassRooms = new List<UserClassRoomDTO>();
                if (handlerResp.Success)
                {
                    // now create a detailed list.
                    foreach (var classRoom in handlerResp.Data)
                    {
                        var UserClassRoomDTO = new UserClassRoomDTO()
                        {
                            ClassRoomId = classRoom.ClassRoomId,
                            classRoomName = classRoom.ClassRoomName,
                            classRoomInchargeID = classRoom.ClassRoomInchargeId,
                            classRoomInchargeLogin = _userHandler.GetUserByUserId(UserId).Data.UserLogin,
                            EnrolledUserId = UserId
                        };
                        enrolledClassRooms.Add(UserClassRoomDTO);
                    }
                }
                return new ApiResponse<List<UserClassRoomDTO>>().CreateApiResponse(
                    handlerResp.Success, enrolledClassRooms,handlerResp.failureMessage
                );
            }
            return new ApiResponse<List<UserClassRoomDTO>>().CreateApiResponse(
                false,
                new List<UserClassRoomDTO>(),
                $"The user - {UserId} is not a valid user"
            );
        }

        public ApiResponse<List<UserDTO>> GetAllUsersEnrolledInClassRoom(Guid classRoomId)
        {
            if(_classRoomMediator.IsVailidClassRoom(classRoomId))
            {
                var handlerResp = _handler.GetAllUsersEnrolledInClassRoom(classRoomId);
                List<UserDTO> enrolledUsers = new List<UserDTO>(); 
                if(handlerResp.Success)
                {
                    foreach(var user in handlerResp.Data)
                    {
                        enrolledUsers.Add(
                            new UserDTO()
                            {
                                UserID = user.UserId,
                                UserLogin = user.UserLogin
                            }
                        );
                    }
                }
                return new ApiResponse<List<UserDTO>>().CreateApiResponse(
                    handlerResp.Success,
                    enrolledUsers,
                    handlerResp.failureMessage
                );

            }
            return new ApiResponse<List<UserDTO>>().CreateApiResponse(
                false,
                new List<UserDTO>(),
                $"classroom - {classRoomId} is an invalid classroom"
            );
        }

        public ApiResponse<bool> RemoveUserFromClassRoom(Guid userId, Guid classRoomId)
        {
            var vaildUser = _userMediator.IsExistingUser(userId);
            var validClassRoom = _classRoomMediator.IsVailidClassRoom(classRoomId);
            if(vaildUser && validClassRoom)
            {
                var handlerResp = _handler.RemoveUserClassRoomJoin(userId, classRoomId);
                return new ApiResponse<bool>().CreateApiResponse(handlerResp.Success, handlerResp.Success, handlerResp.failureMessage);
                
            }
            return new ApiResponse<bool>().CreateApiResponse(false, false, "User or classRoom is invalid");
        }
            

        
    }
}
