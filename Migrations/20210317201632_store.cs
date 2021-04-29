using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class store : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalSupportStorageBig",
                columns: table => new
                {
                    AdditionalSupportStorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdditionalSupportBigID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalSupportStorageBig", x => x.AdditionalSupportStorID);
                });

            migrationBuilder.CreateTable(
                name: "InformationofStorageBig",
                columns: table => new
                {
                    InformationofinterestStorageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InformationofInterestID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformationofStorageBig", x => x.InformationofinterestStorageID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalSupportStorageBig");

            migrationBuilder.DropTable(
                name: "InformationofStorageBig");
        }
    }
}
