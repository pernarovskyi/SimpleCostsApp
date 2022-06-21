using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CostApplication.Migrations
{
    public partial class CostApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Costs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    TypeOfCosts = table.Column<int>(nullable: false),
                    Amount = table.Column<float>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    SensetiveData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    LastVisitedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Costs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
