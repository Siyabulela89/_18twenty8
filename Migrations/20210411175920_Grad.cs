using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class Grad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Graduates",
                columns: table => new
                {
                    GraduateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GraduateName = table.Column<string>(nullable: true),
                    GraduatenameandQual = table.Column<string>(nullable: true),
                    GraduateStoryname = table.Column<string>(nullable: true),
                    pdfstoryurl = table.Column<string>(nullable: true),
                    Videourl = table.Column<string>(nullable: true),
                    imageurl = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Graduates", x => x.GraduateID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Graduates");
        }
    }
}
