using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarBookProject.Migrations
{
    public partial class AddToTableCars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Modifieddate",
                table: "Cars",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateBy",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modifieddate",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "Cars");
        }
    }
}
