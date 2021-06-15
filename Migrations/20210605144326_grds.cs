using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class grds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "RecognitionLetters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order",
                table: "RecognitionLetters");
        }
    }
}
