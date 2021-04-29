using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class grt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VerifCode",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerifCodeComp",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "verifiedRegistration",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerifCode",
                table: "BigSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerifCodeComp",
                table: "BigSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "verifiedRegistration",
                table: "BigSisterDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerifCode",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "VerifCodeComp",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "verifiedRegistration",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "VerifCode",
                table: "BigSisterDetail");

            migrationBuilder.DropColumn(
                name: "VerifCodeComp",
                table: "BigSisterDetail");

            migrationBuilder.DropColumn(
                name: "verifiedRegistration",
                table: "BigSisterDetail");
        }
    }
}
