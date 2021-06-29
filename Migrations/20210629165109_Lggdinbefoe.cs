using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class Lggdinbefoe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "confidentiality",
                table: "Volunteerdetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Loggedinbefore",
                columns: table => new
                {
                    LoggedinBID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loggedinbefore", x => x.LoggedinBID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loggedinbefore");

            migrationBuilder.DropColumn(
                name: "confidentiality",
                table: "Volunteerdetail");
        }
    }
}
