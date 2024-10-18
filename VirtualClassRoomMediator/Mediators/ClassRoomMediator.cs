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
    public class ClassRoomMediator
    {
        private readonly VirtualClassRoomDbContext _dbContext;
        private readonly ClassRoomHandler _handler;
        public ClassRoomMediator(VirtualClassRoomDbContext context)
        {
            _dbContext = context;
            _handler = new ClassRoomHandler(context);
        }

        public ApiResponse<ClassRoomDTO> createClassRoom(ClassRoomDTO classRoomDTO)
        {
            // refacotr this code in a smarter way, current code is crap!!
            // in future check weather the role of the user is teacher.
 
            if(classRoomDTO.ClassRoomId.Equals(Guid.Empty) && new UserMediator(_dbContext).IsExistingTeacher(classRoomDTO.ClassRoomInchargeId))
            {
                var handlerResponse = _handler.CreateClassRoom(classRoomDTO);
                if (handlerResponse.Success)
                {
                    classRoomDTO.ClassRoomId = handlerResponse.Data.ClassRoomId;
                    return new ApiResponse<ClassRoomDTO>().CreateApiResponse(true, classRoomDTO);

                }
                else
                {
                    return new ApiResponse<ClassRoomDTO>().CreateApiResponse(false, classRoomDTO, handlerResponse.failureMessage);
                }
            }
            return new ApiResponse<ClassRoomDTO>().CreateApiResponse(false, classRoomDTO, $"Id should be Null i,e {Guid.Empty.ToString()} or Incharge is invalid user/Teacher");
        }

        public ApiResponse<ClassRoomDTO> updateClassRoom(ClassRoomDTO classRoom)
        {
            if(IsVailidClassRoom(classRoom.ClassRoomId))
            {
                var resp = _handler.UpdateClassRoomDetails(classRoom);
                return new ApiResponse<ClassRoomDTO>().CreateApiResponse(
                        resp.Success,
                        resp.Success ? resp.Data : null,
                        resp.failureMessage
                );
            }
            return new ApiResponse<ClassRoomDTO>().CreateApiResponse(false, new ClassRoomDTO(), $"no Class Room with ID - {classRoom.ClassRoomId}");
        }

        public ApiResponse<ClassRoomDTO> GetClassRoomDetails(Guid classRoomId)
        {
            if(IsVailidClassRoom(classRoomId))
            {
                var resp = _handler.GetClassRoomDetails(classRoomId);
                return new ApiResponse<ClassRoomDTO>().CreateApiResponse(
                    resp.Success,
                    resp.Success ? resp.Data : null,
                    resp.failureMessage
                );
            }
            return new ApiResponse<ClassRoomDTO>().CreateApiResponse(false, new ClassRoomDTO(), $"no Class Room with ID - {classRoomId}");
        }

        public ApiResponse<bool> DeleteClassRoom(Guid classRoomId)
        {
            if (IsVailidClassRoom(classRoomId))
            {
                var handlerResp = _handler.RemoveClassRoom(classRoomId);
                return new ApiResponse<bool>().CreateApiResponse(
                    handlerResp.Success,
                    handlerResp.Success,
                    handlerResp.failureMessage
                );
            }
            return new ApiResponse<bool>().CreateApiResponse(false, false, $"no Class Room with ID - {classRoomId}");
        }

        public ApiResponse<List<ClassRoomDTO>> GetAllClassRooms()
        {
            var classRooms = _handler.GetClassRooms();
            return new ApiResponse<List<ClassRoomDTO>>().CreateApiResponse(
                classRooms.Success,
                classRooms.Success ? convertClassRoomsToDTO(classRooms.Data) : new List<ClassRoomDTO>(),
                classRooms.failureMessage
            );

        }


        internal bool IsVailidClassRoom(Guid classRoomId)
        {
            var  handlerResponse  = _handler.GetClassRoomDetails(classRoomId);
            return handlerResponse.Success;

            /*? new ApiResponse<ClassRoomDTO>().CreateHandlerResponse(true, handlerResponse.Data) :
                new ApiResponse<ClassRoomDTO>().CreateApiResponse(false, handlerResponse.Data, handlerResponse.failureMessage);*/
        }

        private List<ClassRoomDTO> convertClassRoomsToDTO(List<ClassRoomModel> classRoomsRaw)
        {
            List<ClassRoomDTO> classRooms = new List<ClassRoomDTO>();
            foreach(var classRoom in classRoomsRaw)
            {
                classRooms.Add(new ClassRoomDTO()
                {
                    ClassRoomId  = classRoom.ClassRoomId,
                    ClassRoomCount = classRoom.ClassRoomCount,
                    ClassRoomInchargeId = classRoom.ClassRoomInchargeId,
                    ClassRoomName = classRoom.ClassRoomName,
                });
            }
            return classRooms;
        }
    }
}
