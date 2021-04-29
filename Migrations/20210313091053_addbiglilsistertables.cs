using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class addbiglilsistertables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BigSisterDetail",
                columns: table => new
                {
                    BigSisterDetailID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    IDPassport = table.Column<string>(nullable: true),
                    DateofBirth = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    Phonenumber = table.Column<string>(nullable: true),
                    physicaladdress = table.Column<string>(nullable: true),
                    AlternateContact = table.Column<string>(nullable: true),
                    howdidyouhearabout = table.Column<string>(nullable: true),
                    BackgroundQ = table.Column<string>(nullable: true),
                    EverbeenamentorQ = table.Column<int>(nullable: false),
                    DetailsOnEverbeenMentorQ = table.Column<string>(nullable: true),
                    ArrestedConvictedQ = table.Column<int>(nullable: false),
                    DetailsArrestedConvictedQ = table.Column<string>(nullable: true),
                    InformationofInterest = table.Column<int>(nullable: false),
                    Interactionlevelmeet = table.Column<int>(nullable: false),
                    InteractionlevelDigCom = table.Column<int>(nullable: false),
                    prefferedstudentdetails = table.Column<string>(nullable: true),
                    youngerselfQ = table.Column<string>(nullable: true),
                    AdditionalSupport = table.Column<int>(nullable: false),
                    Expectationsonlittlesister = table.Column<string>(nullable: true),
                    ConfirmMentordurationQ = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BigSisterDetail", x => x.BigSisterDetailID);
                });

            migrationBuilder.CreateTable(
                name: "InformationInterest",
                columns: table => new
                {
                    InformationofInterestID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformationInterest", x => x.InformationofInterestID);
                });

            migrationBuilder.CreateTable(
                name: "InteractionLevel",
                columns: table => new
                {
                    InteractionLevelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractionLevel", x => x.InteractionLevelID);
                });

            migrationBuilder.CreateTable(
                name: "LittleSisterDetail",
                columns: table => new
                {
                    LittleSisterDetailID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    IDPassport = table.Column<string>(nullable: true),
                    DateofBirth = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    Phonenumber = table.Column<string>(nullable: true),
                    physicaladdress = table.Column<string>(nullable: true),
                    AlternateContact = table.Column<string>(nullable: true),
                    howdidyouhearaboutQ = table.Column<string>(nullable: true),
                    CurrentStudyQ = table.Column<string>(nullable: true),
                    BackgroundQ = table.Column<string>(nullable: true),
                    EverbeenamenteeQ = table.Column<int>(nullable: false),
                    DetailsOnEverbeenMenteeQ = table.Column<string>(nullable: true),
                    ArrestedConvictedQ = table.Column<int>(nullable: false),
                    DetailsArrestedConvictedQ = table.Column<string>(nullable: true),
                    InformationofInterest = table.Column<int>(nullable: false),
                    Interactionlevelmeet = table.Column<int>(nullable: false),
                    InteractionlevelDigCom = table.Column<int>(nullable: false),
                    prefferedMenteedetails = table.Column<string>(nullable: true),
                    Expectationsonlittlesister = table.Column<string>(nullable: true),
                    ConfirmMenteedurationQ = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LittleSisterDetail", x => x.LittleSisterDetailID);
                });

            migrationBuilder.CreateTable(
                name: "OptionalBool",
                columns: table => new
                {
                    YesNoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionalBool", x => x.YesNoID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BigSisterDetail");

            migrationBuilder.DropTable(
                name: "InformationInterest");

            migrationBuilder.DropTable(
                name: "InteractionLevel");

            migrationBuilder.DropTable(
                name: "LittleSisterDetail");

            migrationBuilder.DropTable(
                name: "OptionalBool");
        }
    }
}
