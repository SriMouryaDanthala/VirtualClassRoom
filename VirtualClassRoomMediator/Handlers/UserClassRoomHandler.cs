using Persistence.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualClassRoomMediator.Handlers
{
    internal class UserClassRoomHandler
    {
        private readonly VirtualClassRoomDbContext _dbContext;
        public UserClassRoomHandler(VirtualClassRoomDbContext context)
        {
            _dbContext = context;
        }

    }
}
