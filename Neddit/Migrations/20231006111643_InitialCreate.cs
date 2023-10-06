using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Neddit.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    threadId = table.Column<float>(type: "REAL", nullable: false),
                    userId = table.Column<float>(type: "REAL", nullable: false),
                    header = table.Column<string>(type: "TEXT", nullable: false),
                    text = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    votes = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.threadId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<float>(type: "REAL", nullable: false),
                    username = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    commentId = table.Column<float>(type: "REAL", nullable: false),
                    userId = table.Column<float>(type: "REAL", nullable: false),
                    text = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    votes = table.Column<int>(type: "INTEGER", nullable: false),
                    threadId = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.commentId);
                    table.ForeignKey(
                        name: "FK_Comments_Threads_threadId",
                        column: x => x.threadId,
                        principalTable: "Threads",
                        principalColumn: "threadId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_threadId",
                table: "Comments",
                column: "threadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Threads");
        }
    }
}
