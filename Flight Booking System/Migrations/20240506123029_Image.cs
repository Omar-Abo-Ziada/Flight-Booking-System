using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_Booking_System.Migrations
{
    /// <inheritdoc />
    public partial class Image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_States_AirPortId",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Countries_AirPortId",
                table: "Countries");

            migrationBuilder.CreateIndex(
                name: "IX_States_AirPortId",
                table: "States",
                column: "AirPortId",
                unique: true,
                filter: "[AirPortId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_AirPortId",
                table: "Countries",
                column: "AirPortId",
                unique: true,
                filter: "[AirPortId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_States_AirPortId",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Countries_AirPortId",
                table: "Countries");

            migrationBuilder.CreateIndex(
                name: "IX_States_AirPortId",
                table: "States",
                column: "AirPortId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_AirPortId",
                table: "Countries",
                column: "AirPortId");
        }
    }
}
