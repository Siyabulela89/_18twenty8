using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class commitees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Committees",
                columns: table => new
                {
                    CommitteeTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Committees", x => x.CommitteeTypeID);
                });

            migrationBuilder.CreateTable(
                name: "CommitteesStorage",
                columns: table => new
                {
                    CommitteeStorageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VolunteerID = table.Column<int>(nullable: false),
                    CommitteeTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitteesStorage", x => x.CommitteeStorageID);
                });

            migrationBuilder.CreateTable(
                name: "Daysofweek",
                columns: table => new
                {
                    DayID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Daysofweek", x => x.DayID);
                });

            migrationBuilder.CreateTable(
                name: "Daysofweekstorage",
                columns: table => new
                {
                    DayStorageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VolunteerID = table.Column<int>(nullable: false),
                    DayID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Daysofweekstorage", x => x.DayStorageID);
                });

            migrationBuilder.CreateTable(
                name: "Programmes",
                columns: table => new
                {
                    ProgrammeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programmes", x => x.ProgrammeID);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammesStorage",
                columns: table => new
                {
                    ProgrammeStorageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VolunteerID = table.Column<int>(nullable: false),
                    ProgrammeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammesStorage", x => x.ProgrammeStorageID);
                });

            migrationBuilder.CreateTable(
                name: "Volunteerdetail",
                columns: table => new
                {
                    VolunteerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fullnames = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    cellnumber = table.Column<string>(nullable: true),
                    Postalcode = table.Column<string>(nullable: true),
                    Postaladdressline1 = table.Column<string>(nullable: true),
                    Postaladdressline2 = table.Column<string>(nullable: true),
                    Province = table.Column<int>(nullable: false),
                    home_businesscontact = table.Column<string>(nullable: true),
                    timetocall = table.Column<int>(nullable: false),
                    Occupation = table.Column<string>(nullable: true),
                    Employer = table.Column<string>(nullable: true),
                    Hoursweekforprogramme = table.Column<int>(nullable: false),
                    Otherintprogrammes = table.Column<string>(nullable: true),
                    previousexperienceinotherorgasvolunteer = table.Column<string>(nullable: true),
                    describewhyyouofferedtovolunteer = table.Column<string>(nullable: true),
                    goaltoachieveinvolunteer = table.Column<string>(nullable: true),
                    Othercommittees = table.Column<string>(nullable: true),
                    describehobbies = table.Column<string>(nullable: true),
                    indemnity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteerdetail", x => x.VolunteerID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Committees");

            migrationBuilder.DropTable(
                name: "CommitteesStorage");

            migrationBuilder.DropTable(
                name: "Daysofweek");

            migrationBuilder.DropTable(
                name: "Daysofweekstorage");

            migrationBuilder.DropTable(
                name: "Programmes");

            migrationBuilder.DropTable(
                name: "ProgrammesStorage");

            migrationBuilder.DropTable(
                name: "Volunteerdetail");
        }
    }
}
