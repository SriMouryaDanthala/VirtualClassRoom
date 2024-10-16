using Persistence.DBContext;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VirtualClassRoomDTO.DTOModels;
using VirtualClassRoomDTO.GenericDataTypes;

namespace VirtualClassRoomMediator.Handlers
{
    internal class ClassRoomHandler
    {
        private readonly VirtualClassRoomDbContext _dbContext;
        public ClassRoomHandler(VirtualClassRoomDbContext context)
        {
            _dbContext = context;
        }
        public HandlerResponse<ClassRoomDTO> CreateClassRoom(ClassRoomDTO NewClassRoom)
        {
            ClassRoomModel ClassRoomModel = new ClassRoomModel()
            {
                ClassRoomId = Guid.NewGuid(),
                ClassRoomName = NewClassRoom.ClassRoomName,
                ClassRoomCount = 0,
                ClassRoomInchargeId = NewClassRoom.ClassRoomInchargeId,
                ClassRoomTimestamp = DateTime.Now.ToUniversalTime(),
            };
            try
            {
                _dbContext.ClassRooms.Add(ClassRoomModel);
                _dbContext.SaveChanges();
                NewClassRoom.ClassRoomId = ClassRoomModel.ClassRoomId;
            }
            catch (Exception ex)
            {
                return new HandlerResponse<ClassRoomDTO>().CreateHandlerResponse(
                    false,
                    NewClassRoom,
                    $"Cannot Create New Classs - {ex.Message}"
                );
            }
            return new HandlerResponse<ClassRoomDTO>().CreateHandlerResponse(
                true,
                NewClassRoom,
                ""
            );
        }

        public HandlerResponse<ClassRoomDTO> UpdateClassRoomDetails(ClassRoomDTO updatedClassRoom)
        {
            ClassRoomModel oldClassRoom = _dbContext.ClassRooms.
                Where(x => x.ClassRoomId.Equals(updatedClassRoom.ClassRoomId)).FirstOrDefault();

            oldClassRoom.ClassRoomName = updatedClassRoom.ClassRoomName;
            oldClassRoom.ClassRoomInchargeId = updatedClassRoom.ClassRoomInchargeId;
            try
            {
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                return new HandlerResponse<ClassRoomDTO>().CreateHandlerResponse(
                    false,
                    updatedClassRoom,
                    $"Cannot update Classs - {ex.Message}"
                );
            }
            return new HandlerResponse<ClassRoomDTO>().CreateHandlerResponse(
                true,
                updatedClassRoom,
                ""
            );
        }

        public HandlerResponse<bool> RemoveClassRoom(Guid classRoomId)
        {
            try
            {
                var classTobeDeleted = _dbContext.ClassRooms.Where(x => x.ClassRoomId == classRoomId).FirstOrDefault();
                _dbContext.ClassRooms.Remove(classTobeDeleted);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new HandlerResponse<bool>().CreateHandlerResponse(
                    false,
                    false,
                    $"Cannot delete Classs {classRoomId.ToString()} - {ex.Message}"
                );
            }
            return new HandlerResponse<bool>().CreateHandlerResponse(true, true);
        }

        public HandlerResponse<ClassRoomDTO> GetClassRoomDetails(Guid classRoomID)
        {
            ClassRoomModel classRoom = null;
            try
            {
                classRoom = _dbContext.ClassRooms.Where(x => x.ClassRoomId == classRoomID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new HandlerResponse<ClassRoomDTO>().CreateHandlerResponse(false, new ClassRoomDTO(), $"{ex.Message}");
            }
            
            return
                new HandlerResponse<ClassRoomDTO>()
                {
                    Success = classRoom != null,
                    Data = new ClassRoomDTO()
                    {
                        ClassRoomId = classRoom != null ? classRoom.ClassRoomId : Guid.Empty,
                        ClassRoomName = classRoom != null ? classRoom.ClassRoomName : "",
                        ClassRoomCount = classRoom != null ? classRoom.ClassRoomCount : 0,

                    },
                    failureMessage = classRoom != null ? "" : $"ClassRoom not found for ID ${classRoomID.ToString()}"
                };
                
        }

        public HandlerResponse<List<ClassRoomModel>> GetClassRooms()
        {
            try
            {
                return new HandlerResponse<List<ClassRoomModel>>().CreateHandlerResponse(true,
                    _dbContext.ClassRooms.ToList(),
                    ""
                );
            }
            catch (Exception ex)
            {
                return new HandlerResponse<List<ClassRoomModel>>().CreateHandlerResponse(false,
                    new List<ClassRoomModel>(),
                    $"cant retrieve classrooms - ${ex.Message}"
                );
            }
        }

    }
}
