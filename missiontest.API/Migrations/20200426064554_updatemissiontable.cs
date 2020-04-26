using Microsoft.EntityFrameworkCore.Migrations;

namespace missiontest.API.Migrations
{
    public partial class updatemissiontable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Mission",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "User");

            migrationBuilder.DropColumn(
                name: "city",
                table: "User");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Mission");
        }
    }
}
