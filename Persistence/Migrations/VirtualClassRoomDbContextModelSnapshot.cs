﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.DBContext;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(VirtualClassRoomDbContext))]
    partial class VirtualClassRoomDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Persistence.Models.ClassRoomModel", b =>
                {
                    b.Property<Guid>("ClassRoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ClassRoomCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("ClassRoomInchargeId")
                        .HasColumnType("uuid");

                    b.Property<string>("ClassRoomName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ClassRoomTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ClassRoomId");

                    b.HasIndex("ClassRoomInchargeId");

                    b.ToTable("ClassRooms");
                });

            modelBuilder.Entity("Persistence.Models.UserClassRoomJoin", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClassRoomId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "ClassRoomId");

                    b.HasIndex("ClassRoomId");

                    b.ToTable("UserClassRoomJoins");
                });

            modelBuilder.Entity("Persistence.Models.UserModel", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("UserLogin")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserRoleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UserTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserId");

                    b.HasIndex("UserRoleId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Persistence.Models.UserRoleModel", b =>
                {
                    b.Property<Guid>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("UserRoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UserRoleTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserRoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Persistence.Models.ClassRoomModel", b =>
                {
                    b.HasOne("Persistence.Models.UserModel", "User")
                        .WithMany("ClassRooms")
                        .HasForeignKey("ClassRoomInchargeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Persistence.Models.UserClassRoomJoin", b =>
                {
                    b.HasOne("Persistence.Models.ClassRoomModel", "ClassRoom")
                        .WithMany("ClassRooms")
                        .HasForeignKey("ClassRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Persistence.Models.UserModel", "User")
                        .WithMany("UserClassRoomJoins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassRoom");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Persistence.Models.UserModel", b =>
                {
                    b.HasOne("Persistence.Models.UserRoleModel", "UserRole")
                        .WithOne("User")
                        .HasForeignKey("Persistence.Models.UserModel", "UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Persistence.Models.ClassRoomModel", b =>
                {
                    b.Navigation("ClassRooms");
                });

            modelBuilder.Entity("Persistence.Models.UserModel", b =>
                {
                    b.Navigation("ClassRooms");

                    b.Navigation("UserClassRoomJoins");
                });

            modelBuilder.Entity("Persistence.Models.UserRoleModel", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
