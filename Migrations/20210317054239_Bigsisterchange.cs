using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class Bigsisterchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalSupport",
                table: "BigSisterDetail");

            migrationBuilder.RenameColumn(
                name: "physicaladdress",
                table: "BigSisterDetail",
                newName: "Interactionlevelmeetother");

            migrationBuilder.RenameColumn(
                name: "InformationofInterest",
                table: "BigSisterDetail",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "AlternateContact",
                table: "BigSisterDetail",
                newName: "InteractionlevelDigComother");

            migrationBuilder.AddColumn<string>(
                name: "AddressStreet",
                table: "BigSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressStreetlinetwo",
                table: "BigSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactNameone",
                table: "BigSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactNametwo",
                table: "BigSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactNumberone",
                table: "BigSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactNumbertwo",
                table: "BigSisterDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressStreet",
                table: "BigSisterDetail");

            migrationBuilder.DropColumn(
                name: "AddressStreetlinetwo",
                table: "BigSisterDetail");

            migrationBuilder.DropColumn(
                name: "EmergencyContactNameone",
                table: "BigSisterDetail");

            migrationBuilder.DropColumn(
                name: "EmergencyContactNametwo",
                table: "BigSisterDetail");

            migrationBuilder.DropColumn(
                name: "EmergencyContactNumberone",
                table: "BigSisterDetail");

            migrationBuilder.DropColumn(
                name: "EmergencyContactNumbertwo",
                table: "BigSisterDetail");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "BigSisterDetail",
                newName: "InformationofInterest");

            migrationBuilder.RenameColumn(
                name: "Interactionlevelmeetother",
                table: "BigSisterDetail",
                newName: "physicaladdress");

            migrationBuilder.RenameColumn(
                name: "InteractionlevelDigComother",
                table: "BigSisterDetail",
                newName: "AlternateContact");

            migrationBuilder.AddColumn<int>(
                name: "AdditionalSupport",
                table: "BigSisterDetail",
                nullable: false,
                defaultValue: 0);
        }
    }
}
