using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class newtablesfa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialSupport",
                columns: table => new
                {
                    FinancialSupportID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    CertifiedID = table.Column<string>(nullable: true),
                    CVurl = table.Column<string>(nullable: true),
                    SiteUrl = table.Column<string>(nullable: true),
                    Academictranscript = table.Column<string>(nullable: true),
                    Proofofregoistrationurl = table.Column<string>(nullable: true),
                    LatestStatementfees = table.Column<string>(nullable: true),
                    VideoURl = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialSupport", x => x.FinancialSupportID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialSupport");
        }
    }
}
