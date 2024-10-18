using Persistence.DBContext;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VirtualClassRoomDTO.DTOModels;
using VirtualClassRoomDTO.GenericDataTypes;

namespace VirtualClassRoomMediator.Handlers
{
    internal class UserClassRoomHandler
    {
        private readonly VirtualClassRoomDbContext _dbContext;
        public UserClassRoomHandler(VirtualClassRoomDbContext context)
        {
            _dbContext = context;
        }

        public HandlerResponse<UserClassRoomJoin> InsertUserClassRoom(Guid UserId, Guid classRoomId)
        {
            UserClassRoomJoin userClassRoom = new UserClassRoomJoin()
            {
                UserId = UserId,
                ClassRoomId = classRoomId,
            };
            try
            {
                _dbContext.UserClassRoomJoins.Add(userClassRoom);
                var classRoom = _dbContext.ClassRooms.Where(c => c.ClassRoomId.Equals(classRoomId)).First();
                classRoom.ClassRoomCount += 1;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new HandlerResponse<UserClassRoomJoin>().CreateHandlerResponse(false, new UserClassRoomJoin(), ex.Message);
            }
            return new HandlerResponse<UserClassRoomJoin>().CreateHandlerResponse(true, userClassRoom);
        }

        public HandlerResponse<UserClassRoomJoin> RemoveUserClassRoomJoin(Guid UserId, Guid classRoomId)
        {
            UserClassRoomJoin enrolledClassRoom;
            try
            {
                enrolledClassRoom = _dbContext.UserClassRoomJoins.Where(x => x.ClassRoomId.Equals(classRoomId) && x.UserId.Equals(UserId)).FirstOrDefault();
                _dbContext.UserClassRoomJoins.Remove(enrolledClassRoom);
                var classRoom = _dbContext.ClassRooms.Where(c => c.ClassRoomId.Equals(classRoomId)).First();
                classRoom.ClassRoomCount -= 1;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new HandlerResponse<UserClassRoomJoin>().CreateHandlerResponse(false, new UserClassRoomJoin(),ex.Message);
            }
            return new HandlerResponse<UserClassRoomJoin>().CreateHandlerResponse(
                !(enrolledClassRoom == null),
                enrolledClassRoom,
                ""
            );
        }

        public HandlerResponse<List<UserModel>> GetAllUsersEnrolledInClassRoom(Guid ClassRoomId)
        {
            List<UserClassRoomJoin> enrolledClassRooms = new List<UserClassRoomJoin>();
            List<UserModel> users = new List<UserModel>();
            try
            {
                enrolledClassRooms = _dbContext.UserClassRoomJoins.Where(u => u.ClassRoomId.Equals(ClassRoomId)).ToList();
                if (enrolledClassRooms.Any())
                {
                    var usersHandler = new UserHandler(_dbContext);
                    foreach (UserClassRoomJoin userClassRoom in enrolledClassRooms)
                    {
                        var user = usersHandler.GetUserByUserId(userClassRoom.UserId);
                        if (user.Success)
                        {
                            users.Add(user.Data);
                        }
                    }
                    usersHandler = null; /* kill the object as soon as the task ends.*/
                }
            }
            catch (Exception ex)
            {
                return new HandlerResponse<List<UserModel>>().CreateHandlerResponse(false, users, ex.Message);
            }
            return new HandlerResponse<List<UserModel>>().CreateHandlerResponse(
                users.Any(),
                users,
                ""
            );
        }

        public HandlerResponse<List<ClassRoomDTO>> GetAllClassRoomsEnrolledByUser(Guid UserId)
        {
            List<UserClassRoomJoin> enrolledClassRooms = new List<UserClassRoomJoin>();
            List<ClassRoomDTO> classRooms = new List<ClassRoomDTO>();
            try
            {
                enrolledClassRooms = _dbContext.UserClassRoomJoins.Where(u => u.UserId.Equals(UserId)).ToList();
                if (enrolledClassRooms.Any())
                {
                    var classRoomsHandler = new ClassRoomHandler(_dbContext);
                    foreach (UserClassRoomJoin userClassRoom in enrolledClassRooms)
                    {
                        var classroom = classRoomsHandler.GetClassRoomDetails(userClassRoom.ClassRoomId);
                        if (classroom.Success)
                        {
                            classRooms.Add(classroom.Data);
                        }
                    }
                    classRoomsHandler = null; /* kill the object as soon as the task ends.*/
                }
            }
            catch (Exception ex)
            {
                return new HandlerResponse<List<ClassRoomDTO>>().CreateHandlerResponse(false, classRooms, ex.Message);
            }
            return new HandlerResponse<List<ClassRoomDTO>>().CreateHandlerResponse(
                classRooms.Any(),
                classRooms,
                ""
            );
        }
    }
}
