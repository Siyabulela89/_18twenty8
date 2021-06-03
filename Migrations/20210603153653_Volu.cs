using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class Volu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CVurl",
                table: "Volunteerdetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IDurl",
                table: "Volunteerdetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVurl",
                table: "Volunteerdetail");

            migrationBuilder.DropColumn(
                name: "IDurl",
                table: "Volunteerdetail");
        }
    }
}
