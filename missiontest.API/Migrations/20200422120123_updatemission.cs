using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace missiontest.API.Migrations
{
    public partial class updatemission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    firstName = table.Column<string>(nullable: false),
                    lastName = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    phone = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    sex = table.Column<string>(nullable: false),
                    token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    missionDate = table.Column<DateTime>(nullable: false),
                    priority = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    isDone = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mission_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mission_userId",
                table: "Mission",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mission");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
