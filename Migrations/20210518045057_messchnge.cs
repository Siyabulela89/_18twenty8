using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _18TWENTY8.Migrations
{
    public partial class messchnge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateSent",
                table: "Messaging",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSent",
                table: "Messaging");
        }
    }
}
