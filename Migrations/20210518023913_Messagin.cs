using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class Messagin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messaging",
                columns: table => new
                {
                    MessageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(nullable: true),
                    EmptyMessage = table.Column<string>(nullable: true),
                    SendUserId = table.Column<string>(nullable: true),
                    ReceiveUserID = table.Column<string>(nullable: true),
                    MsRead = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messaging", x => x.MessageID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messaging");
        }
    }
}
