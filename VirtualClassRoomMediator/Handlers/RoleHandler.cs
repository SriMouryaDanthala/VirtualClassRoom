using Persistence.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualClassRoomDTO.GenericDataTypes;

namespace VirtualClassRoomMediator.Handlers
{
    internal class RoleHandler
    {
        public VirtualClassRoomDbContext _dbContext;
        RoleHandler(VirtualClassRoomDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
