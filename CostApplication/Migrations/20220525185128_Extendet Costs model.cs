using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CostApplication.Migrations
{
    public partial class ExtendetCostsmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Costs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Costs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SensetiveData",
                table: "Costs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "SensetiveData",
                table: "Costs");
        }
    }
}
