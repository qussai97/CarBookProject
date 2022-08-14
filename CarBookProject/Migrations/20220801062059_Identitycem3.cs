using Microsoft.EntityFrameworkCore.Migrations;

namespace CarBookProject.Migrations
{
    public partial class Identitycem3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CompanyAccount",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyAccount",
                table: "AspNetUsers");
        }
    }
}
