using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class litlesiterwork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CertifiedID",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InteractionlevelDigComother",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interactionlevelmeetother",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "profilestatusreason",
                table: "LittleSisterDetail",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LittleSisterAcademic",
                columns: table => new
                {
                    LittleSisterAcademicID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LittleSisterUserID = table.Column<int>(nullable: false),
                    QualificationDocname = table.Column<string>(nullable: true),
                    Qualificationurl = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LittleSisterAcademic", x => x.LittleSisterAcademicID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LittleSisterAcademic");

            migrationBuilder.DropColumn(
                name: "CertifiedID",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "InteractionlevelDigComother",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "Interactionlevelmeetother",
                table: "LittleSisterDetail");

            migrationBuilder.DropColumn(
                name: "profilestatusreason",
                table: "LittleSisterDetail");
        }
    }
}
