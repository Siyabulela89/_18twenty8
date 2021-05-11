using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class FinancialAssistance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationReason",
                table: "FinancialSupport",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationStatusID",
                table: "FinancialSupport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "FinancialSupport",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IdNr_Passport",
                table: "FinancialSupport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imgurl",
                table: "FinancialSupport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "FinancialSupport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "FinancialSupport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerifCode",
                table: "FinancialSupport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerifCodeComp",
                table: "FinancialSupport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "verifiedRegistration",
                table: "FinancialSupport",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationStatus",
                columns: table => new
                {
                    ApplicationStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationStatus", x => x.ApplicationStatusID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationStatus");

            migrationBuilder.DropColumn(
                name: "ApplicationReason",
                table: "FinancialSupport");

            migrationBuilder.DropColumn(
                name: "ApplicationStatusID",
                table: "FinancialSupport");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "FinancialSupport");

            migrationBuilder.DropColumn(
                name: "IdNr_Passport",
                table: "FinancialSupport");

            migrationBuilder.DropColumn(
                name: "Imgurl",
                table: "FinancialSupport");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "FinancialSupport");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "FinancialSupport");

            migrationBuilder.DropColumn(
                name: "VerifCode",
                table: "FinancialSupport");

            migrationBuilder.DropColumn(
                name: "VerifCodeComp",
                table: "FinancialSupport");

            migrationBuilder.DropColumn(
                name: "verifiedRegistration",
                table: "FinancialSupport");
        }
    }
}
