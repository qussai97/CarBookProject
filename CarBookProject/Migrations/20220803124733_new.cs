using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarBookProject.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CostPerDay",
                table: "Cars",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "CarImg",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    BookingCarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From_dt_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ret_dt_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stutus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostPerDay = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    Carid = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.BookingCarId);
                    table.ForeignKey(
                        name: "FK_bookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bookings_Cars_Carid",
                        column: x => x.Carid,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_Carid",
                table: "bookings",
                column: "Carid");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_UserId",
                table: "bookings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropColumn(
                name: "CarImg",
                table: "Cars");

            migrationBuilder.AlterColumn<decimal>(
                name: "CostPerDay",
                table: "Cars",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
