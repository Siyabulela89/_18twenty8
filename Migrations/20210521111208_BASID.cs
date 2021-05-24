using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class BASID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BursaryApplicationCandidate",
                columns: table => new
                {
                    BACID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<string>(nullable: true),
                    BursaryID = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ReceiveUserID = table.Column<string>(nullable: true),
                    DateApplied = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BursaryApplicationCandidate", x => x.BACID);
                });

            migrationBuilder.CreateTable(
                name: "BursaryApplicationStatusApp",
                columns: table => new
                {
                    BASAID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BursaryApplicationStatusApp", x => x.BASAID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BursaryApplicationCandidate");

            migrationBuilder.DropTable(
                name: "BursaryApplicationStatusApp");
        }
    }
}
