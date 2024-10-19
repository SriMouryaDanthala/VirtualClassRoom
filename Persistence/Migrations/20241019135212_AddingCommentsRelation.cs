using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingCommentsRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentClassRoom = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentContent = table.Column<string>(type: "text", nullable: false),
                    CommentType = table.Column<Guid>(type: "uuid", nullable: false),
                    ReplyToComment = table.Column<Guid>(type: "uuid", nullable: true),
                    CommentTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_ClassRooms_CommentClassRoom",
                        column: x => x.CommentClassRoom,
                        principalTable: "ClassRooms",
                        principalColumn: "ClassRoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_CommentUserId",
                        column: x => x.CommentUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentClassRoom",
                table: "Comments",
                column: "CommentClassRoom");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentUserId",
                table: "Comments",
                column: "CommentUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");
        }
    }
}
