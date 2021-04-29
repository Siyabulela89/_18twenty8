using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class Newstatustables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignApprove",
                columns: table => new
                {
                    AssAppID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignApprove", x => x.AssAppID);
                });

            migrationBuilder.CreateTable(
                name: "SisterAssignment",
                columns: table => new
                {
                    SisAssID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LittleSisterID = table.Column<int>(nullable: false),
                    BigSisterID = table.Column<int>(nullable: false),
                    BigApproveID = table.Column<int>(nullable: false),
                    LittleApprove = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SisterAssignment", x => x.SisAssID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignApprove");

            migrationBuilder.DropTable(
                name: "SisterAssignment");
        }
    }
}
