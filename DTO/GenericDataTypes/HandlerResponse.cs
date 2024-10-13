using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualClassRoomDTO.GenericDataTypes
{
    public class HandlerResponse<T>
    {
        public bool Success { get; set; }
        public string failureMessage { get; set; }
        public T Data { get; set; }

        public HandlerResponse<T> CreateHandlerResponse(bool success, T Data , string failureMessage = "")
        {
            return new HandlerResponse<T>
            {
                Success = success,
                Data = Data,
                failureMessage = failureMessage
            };
        }
    }

    
}
