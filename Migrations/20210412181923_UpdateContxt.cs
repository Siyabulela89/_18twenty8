using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class UpdateContxt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CVUrl",
                table: "BigSisterDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertifiedID",
                table: "BigSisterDetail",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BigSisterAcademic",
                columns: table => new
                {
                    BigSisterAcademicID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QualificationDocname = table.Column<string>(nullable: true),
                    Qualificationurl = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BigSisterAcademic", x => x.BigSisterAcademicID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BigSisterAcademic");

            migrationBuilder.DropColumn(
                name: "CVUrl",
                table: "BigSisterDetail");

            migrationBuilder.DropColumn(
                name: "CertifiedID",
                table: "BigSisterDetail");
        }
    }
}
