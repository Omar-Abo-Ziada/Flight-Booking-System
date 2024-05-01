using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_Booking_System.Migrations
{
    /// <inheritdoc />
    public partial class Flight_In_Ticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Duration",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FlightId",
                table: "Tickets",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Flights_FlightId",
                table: "Tickets",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Flights_FlightId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_FlightId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Flights");
        }
    }
}
