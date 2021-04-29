using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class upd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imageurl",
                table: "BigSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "BigSisterDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imageurl",
                table: "BigSisterDetail");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "BigSisterDetail");
        }
    }
}
