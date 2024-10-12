using Microsoft.EntityFrameworkCore;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DBContext
{
    public class VirtualClassRoomDbContext : DbContext
    {
        public VirtualClassRoomDbContext(DbContextOptions options) : base(options) 
        {
            
        }
        public DbSet<UserRoleModel> UserRoles { get; set; }
    }
}
