using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class Finnce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CellphoneNr",
                table: "FinancialSupport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "FinancialSupport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalBio",
                table: "FinancialSupport",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellphoneNr",
                table: "FinancialSupport");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "FinancialSupport");

            migrationBuilder.DropColumn(
                name: "PersonalBio",
                table: "FinancialSupport");
        }
    }
}
