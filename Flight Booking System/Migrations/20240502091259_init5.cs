using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flight_Booking_System.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirPorts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirPortNumber = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirPorts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<float>(type: "real", nullable: true),
                    Height = table.Column<float>(type: "real", nullable: true),
                    WingSpan = table.Column<float>(type: "real", nullable: true),
                    Engine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Section = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AirLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineNumber = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirLines_AirPorts_AirportId",
                        column: x => x.AirportId,
                        principalTable: "AirPorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Places_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Places_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartId = table.Column<int>(type: "int", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    DestiantionId = table.Column<int>(type: "int", nullable: true),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    PlaneId = table.Column<int>(type: "int", nullable: true),
                    AirLineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_AirLines_AirLineId",
                        column: x => x.AirLineId,
                        principalTable: "AirLines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Flights_Places_DestiantionId",
                        column: x => x.DestiantionId,
                        principalTable: "Places",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Flights_Places_StartId",
                        column: x => x.StartId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Flights_Planes_PlaneId",
                        column: x => x.PlaneId,
                        principalTable: "Planes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Class = table.Column<int>(type: "int", nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: true),
                    SeatId = table.Column<int>(type: "int", nullable: true),
                    FlightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    IsChild = table.Column<bool>(type: "bit", nullable: false),
                    PassportNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true),
                    FlightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passengers_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Passengers_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AirPorts",
                columns: new[] { "Id", "AirPortNumber", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Cairo AirPort" },
                    { 2, 2, "Frankfurt AirPort" },
                    { 3, 3, "Sydney AirPort" },
                    { 4, 4, "Dubai AirPort" },
                    { 5, 5, "Atlanta AirPort" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Egypt" },
                    { 2, "USA" },
                    { 3, "Germany" },
                    { 4, "Australia" },
                    { 5, "Japan" }
                });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "Id", "Engine", "Height", "Length", "Name", "WingSpan", "capacity" },
                values: new object[,]
                {
                    { 1, "CFM56-7B", 41f, 110f, "Boeing 737", 117f, 188 },
                    { 2, "CFM56-5B4", 39f, 123f, "Airbus A320", 117f, 180 },
                    { 3, "GE90-115B", 61f, 242f, "Boeing 777", 199f, 396 },
                    { 4, "Trent 900", 79f, 238f, "Airbus A380", 261f, 853 }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "Number", "Section" },
                values: new object[,]
                {
                    { 1, 1, 4 },
                    { 2, 2, 0 },
                    { 3, 3, 1 },
                    { 4, 4, 6 },
                    { 5, 5, 2 },
                    { 6, 6, 5 },
                    { 7, 7, 4 },
                    { 8, 8, 2 },
                    { 9, 9, 1 },
                    { 10, 10, 4 }
                });

            migrationBuilder.InsertData(
                table: "AirLines",
                columns: new[] { "Id", "AirlineNumber", "AirportId", "Name" },
                values: new object[,]
                {
                    { 1, 5, 1, "EgyptAirs" },
                    { 2, 10, 2, "Lufthansa" },
                    { 3, 15, 3, "Qantas" },
                    { 4, 20, 4, "Emirates" },
                    { 5, 25, 5, "Delta" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Cairo" },
                    { 2, 1, "Alexandria" },
                    { 3, 1, "Aswan" },
                    { 4, 2, "Texas" },
                    { 5, 2, "California" }
                });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Id", "CountryId", "StateId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "AirLineId", "ArrivalTime", "DepartureTime", "DestiantionId", "DestinationId", "Duration", "PlaneId", "StartId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 12, 25, 16, 45, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 25, 10, 30, 0, 0, DateTimeKind.Unspecified), null, 1, new TimeSpan(0, 6, 15, 0, 0), 1, 1 },
                    { 2, null, new DateTime(2024, 12, 30, 20, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 30, 14, 0, 0, 0, DateTimeKind.Unspecified), null, 2, new TimeSpan(0, 6, 0, 0, 0), 2, 2 },
                    { 3, null, new DateTime(2024, 11, 15, 12, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), null, 3, new TimeSpan(0, 3, 15, 0, 0), 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Class", "FlightId", "PassengerId", "Price", "SeatId" },
                values: new object[,]
                {
                    { 1, 0, 1, 1, 299.99m, 1 },
                    { 2, 1, 1, 2, 499.99m, 2 },
                    { 3, 2, 2, 3, 1299.99m, 3 },
                    { 4, 0, 2, 4, 350.00m, 4 },
                    { 5, 1, 3, 5, 850.00m, 5 }
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "Id", "Age", "FlightId", "Gender", "IsChild", "Name", "NationalId", "PassportNum", "TicketId" },
                values: new object[,]
                {
                    { 1, 22, null, 1, false, "Joy", "302245", "52546874", 1 },
                    { 2, 30, null, 0, false, "Bob", "547289", "63546844", 2 },
                    { 3, 27, null, 1, false, "Alice", "223456", "48567234", 3 },
                    { 4, 12, null, 0, true, "Charlie", "567890", "58694230", 4 },
                    { 5, 45, null, 1, false, "Diana", "987654", "11223344", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirLines_AirportId",
                table: "AirLines",
                column: "AirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirLineId",
                table: "Flights",
                column: "AirLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DestiantionId",
                table: "Flights",
                column: "DestiantionId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PlaneId",
                table: "Flights",
                column: "PlaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_StartId",
                table: "Flights",
                column: "StartId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_FlightId",
                table: "Passengers",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_TicketId",
                table: "Passengers",
                column: "TicketId",
                unique: true,
                filter: "[TicketId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Places_CountryId",
                table: "Places",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_StateId",
                table: "Places",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FlightId",
                table: "Tickets",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatId",
                table: "Tickets",
                column: "SeatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "AirLines");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "AirPorts");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
