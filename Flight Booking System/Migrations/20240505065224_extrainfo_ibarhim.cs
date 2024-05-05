using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flight_Booking_System.Migrations
{
    /// <inheritdoc />
    public partial class extrainfo_ibarhim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StartId",
                table: "Flights",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "Flights",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepartureTime",
                table: "Flights",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ArrivalTime",
                table: "Flights",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AirPorts",
                columns: new[] { "Id", "AirPortNumber", "Name" },
                values: new object[,]
                {
                    { 6, 6, "Heathrow Airport" },
                    { 7, 7, "Charles de Gaulle Airport" },
                    { 8, 8, "Los Angeles International Airport" },
                    { 9, 9, "O'Hare International Airport" },
                    { 10, 10, "Changi Airport" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "PlaceId" },
                values: new object[,]
                {
                    { 6, "United Kingdom", null },
                    { 7, "France", null },
                    { 8, "United States", null },
                    { 9, "Singapore", null },
                    { 10, "Qatar", null }
                });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "Id", "Engine", "FlightId", "Height", "Length", "Name", "WingSpan", "capacity" },
                values: new object[,]
                {
                    { 5, "GEnx-1B", null, 56f, 206f, "Boeing 787", 197f, 242 },
                    { 6, "Trent XWB", null, 56f, 227f, "Airbus A350", 212f, 325 },
                    { 7, "CF6-80", null, 48f, 201f, "Boeing 767", 156f, 375 },
                    { 8, "Trent 700", null, 58f, 63f, "Airbus A330", 197f, 440 },
                    { 9, "RB211-535", null, 44f, 54f, "Boeing 757", 38f, 295 }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "Number", "Section", "TicketId" },
                values: new object[,]
                {
                    { 11, 11, 4, null },
                    { 12, 12, 0, null },
                    { 13, 13, 1, null },
                    { 14, 14, 6, null },
                    { 15, 15, 2, null }
                });

            migrationBuilder.InsertData(
                table: "AirLines",
                columns: new[] { "Id", "AirlineNumber", "AirportId", "Name" },
                values: new object[,]
                {
                    { 6, 30, 6, "British Airways" },
                    { 7, 35, 7, "Air France" },
                    { 8, 40, 8, "American Airlines" },
                    { 9, 45, 9, "United Airlines" },
                    { 10, 50, 10, "Singapore Airlines" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "AirLineId", "ArrivalTime", "DepartureTime", "DestinationId", "Duration", "PlaneId", "StartId", "imageURL" },
                values: new object[,]
                {
                    { 4, null, new DateTime(2024, 12, 25, 16, 45, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 25, 10, 30, 0, 0, DateTimeKind.Unspecified), 4, new TimeSpan(0, 6, 15, 0, 0), 5, 4, null },
                    { 5, null, new DateTime(2024, 12, 30, 20, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 30, 14, 0, 0, 0, DateTimeKind.Unspecified), 5, new TimeSpan(0, 6, 0, 0, 0), 6, 5, null }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "CountryId", "Name", "PlaceId" },
                values: new object[,]
                {
                    { 6, 6, "England", null },
                    { 7, 6, "Scotland", null },
                    { 8, 6, "Wales", null },
                    { 9, 7, "Île-de-France", null },
                    { 10, 7, "Provence-Alpes-Côte d'Azur", null }
                });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Id", "CountryId", "StateId" },
                values: new object[,]
                {
                    { 6, 6, 6 },
                    { 7, 6, 7 },
                    { 8, 6, 8 },
                    { 9, 7, 9 },
                    { 10, 7, 10 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Class", "FlightId", "PassengerId", "Price", "SeatId" },
                values: new object[,]
                {
                    { 6, 0, 4, 6, 399.99m, 11 },
                    { 7, 1, 5, 7, 599.99m, 12 },
                    { 9, 0, 4, 9, 380.00m, 14 },
                    { 10, 1, 5, 10, 900.00m, 15 }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "AirLineId", "ArrivalTime", "DepartureTime", "DestinationId", "Duration", "PlaneId", "StartId", "imageURL" },
                values: new object[] { 6, null, new DateTime(2024, 11, 15, 12, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 6, new TimeSpan(0, 3, 15, 0, 0), 7, 6, null });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "Id", "Age", "FlightId", "Gender", "IsChild", "Name", "NationalId", "PassportNum", "TicketId" },
                values: new object[,]
                {
                    { 6, 35, null, 0, false, "John", "789012", "77889900", 6 },
                    { 7, 29, null, 1, false, "Sophia", "567890", "11223344", 7 },
                    { 9, 8, null, 1, true, "Olivia", "456789", "99001122", 9 },
                    { 10, 55, null, 0, false, "James", "234567", "33445566", 10 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Class", "FlightId", "PassengerId", "Price", "SeatId" },
                values: new object[] { 8, 2, 6, 8, 1399.99m, 13 });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "Id", "Age", "FlightId", "Gender", "IsChild", "Name", "NationalId", "PassportNum", "TicketId" },
                values: new object[] { 8, 45, null, 0, false, "William", "345678", "55667788", 8 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AirLines",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AirLines",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AirLines",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AirLines",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AirLines",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Passengers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Passengers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Passengers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Passengers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Passengers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AirPorts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AirPorts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AirPorts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AirPorts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AirPorts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<int>(
                name: "StartId",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepartureTime",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ArrivalTime",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
