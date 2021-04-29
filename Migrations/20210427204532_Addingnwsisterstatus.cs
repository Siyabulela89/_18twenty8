using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class Addingnwsisterstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LittleApprove",
                table: "SisterAssignment",
                newName: "LittleApproveID");

            migrationBuilder.AddColumn<int>(
                name: "AssignSisterStatusID",
                table: "SisterAssignment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AssignSisterStatus",
                columns: table => new
                {
                    AssignSisterStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignSisterStatus", x => x.AssignSisterStatusID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignSisterStatus");

            migrationBuilder.DropColumn(
                name: "AssignSisterStatusID",
                table: "SisterAssignment");

            migrationBuilder.RenameColumn(
                name: "LittleApproveID",
                table: "SisterAssignment",
                newName: "LittleApprove");
        }
    }
}
