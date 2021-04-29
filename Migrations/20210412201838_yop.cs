using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class yop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "physicaladdress",
                table: "LittleSisterDetail",
                newName: "UserID");

            migrationBuilder.AddColumn<string>(
                name: "AddressStreet",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressStreetlinetwo",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CVurl",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactNameone",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactNametwo",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactNumberone",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactNumbertwo",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imageurl",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Province",
                table: "LittleSisterDetail",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressStreet",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "AddressStreetlinetwo",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "CVurl",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "EmergencyContactNameone",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "EmergencyContactNametwo",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "EmergencyContactNumberone",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "EmergencyContactNumbertwo",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "Imageurl",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "LittleSisterDetail");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "LittleSisterDetail",
                newName: "physicaladdress");
        }
    }
}
