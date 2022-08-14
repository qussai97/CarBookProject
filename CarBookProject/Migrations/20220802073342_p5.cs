using Microsoft.EntityFrameworkCore.Migrations;

namespace CarBookProject.Migrations
{
    public partial class p5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "venu",
                table: "Cars",
                newName: "Venu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Venu",
                table: "Cars",
                newName: "venu");
        }
    }
}
