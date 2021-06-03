using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class recoletters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "logourl",
                table: "RecognitionLetters",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BursaryTypeID",
                table: "BursaryApplication",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "externallink",
                table: "BursaryApplication",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BursaryType",
                columns: table => new
                {
                    BursaryTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BursaryType", x => x.BursaryTypeID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BursaryType");

            migrationBuilder.DropColumn(
                name: "logourl",
                table: "RecognitionLetters");

            migrationBuilder.DropColumn(
                name: "BursaryTypeID",
                table: "BursaryApplication");

            migrationBuilder.DropColumn(
                name: "externallink",
                table: "BursaryApplication");
        }
    }
}
