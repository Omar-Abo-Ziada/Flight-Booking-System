using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_Booking_System.Migrations
{
    /// <inheritdoc />
    public partial class aftereditairportmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AirPortNumber",
                table: "AirPorts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AirPorts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirPortNumber",
                table: "AirPorts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AirPorts");
        }
    }
}
