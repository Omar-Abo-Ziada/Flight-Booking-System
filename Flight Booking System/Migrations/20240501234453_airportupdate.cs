using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight_Booking_System.Migrations
{
    /// <inheritdoc />
    public partial class airportupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirLines_AirPorts_AirPortId",
                table: "AirLines");

            migrationBuilder.RenameColumn(
                name: "AirPortId",
                table: "AirLines",
                newName: "AirportId");

            migrationBuilder.RenameIndex(
                name: "IX_AirLines_AirPortId",
                table: "AirLines",
                newName: "IX_AirLines_AirportId");

            migrationBuilder.AlterColumn<int>(
                name: "AirportId",
                table: "AirLines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AirlineNumber",
                table: "AirLines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AirLines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AirLines_AirPorts_AirportId",
                table: "AirLines",
                column: "AirportId",
                principalTable: "AirPorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirLines_AirPorts_AirportId",
                table: "AirLines");

            migrationBuilder.DropColumn(
                name: "AirlineNumber",
                table: "AirLines");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AirLines");

            migrationBuilder.RenameColumn(
                name: "AirportId",
                table: "AirLines",
                newName: "AirPortId");

            migrationBuilder.RenameIndex(
                name: "IX_AirLines_AirportId",
                table: "AirLines",
                newName: "IX_AirLines_AirPortId");

            migrationBuilder.AlterColumn<int>(
                name: "AirPortId",
                table: "AirLines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AirLines_AirPorts_AirPortId",
                table: "AirLines",
                column: "AirPortId",
                principalTable: "AirPorts",
                principalColumn: "Id");
        }
    }
}
