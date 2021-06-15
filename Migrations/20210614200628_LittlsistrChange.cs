using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class LittlsistrChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ConfirmMenteedurationQ",
                table: "LittleSisterDetail",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ConfirmMenteedurationQ",
                table: "LittleSisterDetail",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
