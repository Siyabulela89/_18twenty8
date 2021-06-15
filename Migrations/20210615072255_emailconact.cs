using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class emailconact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMessage",
                columns: table => new
                {
                    mesid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    to = table.Column<string>(nullable: true),
                    From = table.Column<string>(nullable: true),
                    Fromemail = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    content = table.Column<string>(nullable: true),
                    IsHtml = table.Column<bool>(nullable: false),
                    np = table.Column<string>(nullable: true),
                    nt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMessage", x => x.mesid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMessage");
        }
    }
}
