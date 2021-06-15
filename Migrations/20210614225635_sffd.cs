using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class sffd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "otherhobbies",
                table: "LittleSisterDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "otherhobbies",
                table: "LittleSisterDetail");
        }
    }
}
