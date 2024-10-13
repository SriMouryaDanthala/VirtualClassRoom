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
        public DbSet<UserRoleModel> UserRoles { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ClassRoomModel> ClassRooms { get; set; }
        public DbSet<UserClassRoomJoin> UserClassRoomJoins { get; set; }
        public VirtualClassRoomDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region many to many relationship for UserClasRoomJoin
            modelBuilder.Entity<UserClassRoomJoin>()
                .HasKey(ucj => new { ucj.UserId, ucj.ClassRoomId });

            modelBuilder.Entity<UserClassRoomJoin>()
                .HasOne(sc => sc.User)
                .WithMany(sc => sc.UserClassRoomJoins)
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserClassRoomJoin>()
                .HasOne(sc => sc.ClassRoom)
                .WithMany(sc => sc.ClassRooms)
                .HasForeignKey(sc => sc.ClassRoomId)
                .OnDelete(DeleteBehavior.Cascade); ;
            #endregion

        }
    }
}

