using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flight_Booking_System.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AirLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineNumber = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirLines_AirPorts_AirportId",
                        column: x => x.AirportId,
                        principalTable: "AirPorts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartId = table.Column<int>(type: "int", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: true),
                    Length = table.Column<float>(type: "real", nullable: true),
                    Height = table.Column<float>(type: "real", nullable: true),
                    WingSpan = table.Column<float>(type: "real", nullable: true),
                    Engine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planes_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
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

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Section = table.Column<int>(type: "int", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
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
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    PlaceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_States_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
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
                columns: new[] { "Id", "Name", "PlaceId" },
                values: new object[,]
                {
                    { 1, "Egypt", null },
                    { 2, "USA", null },
                    { 3, "Germany", null },
                    { 4, "Australia", null },
                    { 5, "Japan", null }
                });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "Id", "Engine", "FlightId", "Height", "Length", "Name", "WingSpan", "capacity" },
                values: new object[,]
                {
                    { 1, "CFM56-7B", null, 41f, 110f, "Boeing 737", 117f, 188 },
                    { 2, "CFM56-5B4", null, 39f, 123f, "Airbus A320", 117f, 180 },
                    { 3, "GE90-115B", null, 61f, 242f, "Boeing 777", 199f, 396 },
                    { 4, "Trent 900", null, 79f, 238f, "Airbus A380", 261f, 853 }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "Number", "Section", "TicketId" },
                values: new object[,]
                {
                    { 1, 1, 4, null },
                    { 2, 2, 0, null },
                    { 3, 3, 1, null },
                    { 4, 4, 6, null },
                    { 5, 5, 2, null },
                    { 6, 6, 5, null },
                    { 7, 7, 4, null },
                    { 8, 8, 2, null },
                    { 9, 9, 1, null },
                    { 10, 10, 4, null }
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
                columns: new[] { "Id", "CountryId", "Name", "PlaceId" },
                values: new object[,]
                {
                    { 1, 1, "Cairo", null },
                    { 2, 1, "Alexandria", null },
                    { 3, 1, "Aswan", null },
                    { 4, 2, "Texas", null },
                    { 5, 2, "California", null }
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
                columns: new[] { "Id", "AirLineId", "ArrivalTime", "DepartureTime", "DestinationId", "Duration", "PlaneId", "StartId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 12, 25, 16, 45, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 25, 10, 30, 0, 0, DateTimeKind.Unspecified), 1, new TimeSpan(0, 6, 15, 0, 0), 1, 1 },
                    { 2, null, new DateTime(2024, 12, 30, 20, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 30, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, new TimeSpan(0, 6, 0, 0, 0), 2, 2 },
                    { 3, null, new DateTime(2024, 11, 15, 12, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 3, new TimeSpan(0, 3, 15, 0, 0), 3, 3 }
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_PlaceId",
                table: "Countries",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirLineId",
                table: "Flights",
                column: "AirLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DestinationId",
                table: "Flights",
                column: "DestinationId");

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
                name: "IX_Planes_FlightId",
                table: "Planes",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_TicketId",
                table: "Seats",
                column: "TicketId",
                unique: true,
                filter: "[TicketId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_States_PlaceId",
                table: "States",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FlightId",
                table: "Tickets",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Places_PlaceId",
                table: "Countries",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Places_DestinationId",
                table: "Flights",
                column: "DestinationId",
                principalTable: "Places",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Places_StartId",
                table: "Flights",
                column: "StartId",
                principalTable: "Places",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Planes_PlaneId",
                table: "Flights",
                column: "PlaneId",
                principalTable: "Planes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_States_StateId",
                table: "Places",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirLines_AirPorts_AirportId",
                table: "AirLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Places_PlaceId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Places_DestinationId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Places_StartId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Places_PlaceId",
                table: "States");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_AirLines_AirLineId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Planes_PlaneId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "AirPorts");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "AirLines");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
