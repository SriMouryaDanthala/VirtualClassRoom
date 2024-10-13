using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassRoomModel_UserModel_ClassRoomInchargeId",
                table: "ClassRoomModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserModel_UserRoles_UserRoleId",
                table: "UserModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserModel",
                table: "UserModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassRoomModel",
                table: "ClassRoomModel");

            migrationBuilder.RenameTable(
                name: "UserModel",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "ClassRoomModel",
                newName: "ClassRooms");

            migrationBuilder.RenameIndex(
                name: "IX_UserModel_UserRoleId",
                table: "Users",
                newName: "IX_Users_UserRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRoomModel_ClassRoomInchargeId",
                table: "ClassRooms",
                newName: "IX_ClassRooms_ClassRoomInchargeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassRooms",
                table: "ClassRooms",
                column: "ClassRoomId");

            migrationBuilder.CreateTable(
                name: "UserClassRoomJoins",
                columns: table => new
                {
                    ClassRoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClassRoomJoins", x => new { x.UserId, x.ClassRoomId });
                    table.ForeignKey(
                        name: "FK_UserClassRoomJoins_ClassRooms_ClassRoomId",
                        column: x => x.ClassRoomId,
                        principalTable: "ClassRooms",
                        principalColumn: "ClassRoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserClassRoomJoins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClassRoomJoins_ClassRoomId",
                table: "UserClassRoomJoins",
                column: "ClassRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRooms_Users_ClassRoomInchargeId",
                table: "ClassRooms",
                column: "ClassRoomInchargeId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserRoles_UserRoleId",
                table: "Users",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "UserRoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassRooms_Users_ClassRoomInchargeId",
                table: "ClassRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRoles_UserRoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserClassRoomJoins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassRooms",
                table: "ClassRooms");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserModel");

            migrationBuilder.RenameTable(
                name: "ClassRooms",
                newName: "ClassRoomModel");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserRoleId",
                table: "UserModel",
                newName: "IX_UserModel_UserRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassRooms_ClassRoomInchargeId",
                table: "ClassRoomModel",
                newName: "IX_ClassRoomModel_ClassRoomInchargeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserModel",
                table: "UserModel",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassRoomModel",
                table: "ClassRoomModel",
                column: "ClassRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassRoomModel_UserModel_ClassRoomInchargeId",
                table: "ClassRoomModel",
                column: "ClassRoomInchargeId",
                principalTable: "UserModel",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserModel_UserRoles_UserRoleId",
                table: "UserModel",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "UserRoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
