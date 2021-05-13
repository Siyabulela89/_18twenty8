using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class BursApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BursaryApplication",
                columns: table => new
                {
                    BursaryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    ApplicationStartDate = table.Column<DateTime>(nullable: false),
                    ApplicationEndDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    QualifyingCriteria = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BursaryApplication", x => x.BursaryID);
                });

            migrationBuilder.CreateTable(
                name: "BursaryApplicationRequest",
                columns: table => new
                {
                    BARID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserIDApp = table.Column<string>(nullable: true),
                    BursaryStatus = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BursaryApplicationRequest", x => x.BARID);
                });

            migrationBuilder.CreateTable(
                name: "BursaryStatus",
                columns: table => new
                {
                    BursaryStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BursaryStatus", x => x.BursaryStatusID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BursaryApplication");

            migrationBuilder.DropTable(
                name: "BursaryApplicationRequest");

            migrationBuilder.DropTable(
                name: "BursaryStatus");
        }
    }
}
