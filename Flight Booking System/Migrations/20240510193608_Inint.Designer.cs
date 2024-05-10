﻿// <auto-generated />
using System;
using Flight_Booking_System.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Flight_Booking_System.Migrations
{
    [DbContext(typeof(ITIContext))]
    [Migration("20240510193608_Inint")]
    partial class Inint
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Flight_Booking_System.Models.AirPort", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AirPortNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AirPorts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AirPortNumber = 1,
                            Name = "Cairo AirPort"
                        },
                        new
                        {
                            Id = 2,
                            AirPortNumber = 2,
                            Name = "Frankfurt AirPort"
                        },
                        new
                        {
                            Id = 3,
                            AirPortNumber = 3,
                            Name = "Sydney AirPort"
                        },
                        new
                        {
                            Id = 4,
                            AirPortNumber = 4,
                            Name = "Dubai AirPort"
                        },
                        new
                        {
                            Id = 5,
                            AirPortNumber = 5,
                            Name = "Atlanta AirPort"
                        });
                });

            modelBuilder.Entity("Flight_Booking_System.Models.ApplicationUSer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int?>("PassengerId")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PassengerId")
                        .IsUnique()
                        .HasFilter("[PassengerId] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AirPortId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AirPortId")
                        .IsUnique()
                        .HasFilter("[AirPortId] IS NOT NULL");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AirPortId = 1,
                            Name = "Egypt"
                        },
                        new
                        {
                            Id = 2,
                            AirPortId = 2,
                            Name = "USA"
                        },
                        new
                        {
                            Id = 3,
                            AirPortId = 3,
                            Name = "Germany"
                        },
                        new
                        {
                            Id = 4,
                            AirPortId = 4,
                            Name = "Australia"
                        },
                        new
                        {
                            Id = 5,
                            AirPortId = 5,
                            Name = "Japan"
                        });
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DestinationAirportId")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("time");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("SourceAirportId")
                        .HasColumnType("int");

                    b.Property<string>("imageURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DestinationAirportId");

                    b.HasIndex("SourceAirportId");

                    b.ToTable("Flights");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ArrivalTime = new DateTime(2024, 12, 25, 16, 45, 0, 0, DateTimeKind.Unspecified),
                            DepartureTime = new DateTime(2024, 12, 25, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            DestinationAirportId = 2,
                            Duration = new TimeSpan(0, 6, 15, 0, 0),
                            IsActive = false,
                            SourceAirportId = 1
                        },
                        new
                        {
                            Id = 2,
                            ArrivalTime = new DateTime(2024, 12, 30, 20, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureTime = new DateTime(2024, 12, 30, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            DestinationAirportId = 1,
                            Duration = new TimeSpan(0, 6, 0, 0, 0),
                            IsActive = false,
                            SourceAirportId = 2
                        },
                        new
                        {
                            Id = 3,
                            ArrivalTime = new DateTime(2024, 11, 15, 12, 15, 0, 0, DateTimeKind.Unspecified),
                            DepartureTime = new DateTime(2024, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            DestinationAirportId = 1,
                            Duration = new TimeSpan(0, 3, 15, 0, 0),
                            IsActive = false,
                            SourceAirportId = 3
                        });
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Passenger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("FlightId")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsChild")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassportNum")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("Passengers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 22,
                            FlightId = 1,
                            Gender = 1,
                            IsChild = false,
                            Name = "Joy",
                            NationalId = "302245",
                            PassportNum = "52546874"
                        },
                        new
                        {
                            Id = 2,
                            Age = 30,
                            FlightId = 1,
                            Gender = 0,
                            IsChild = false,
                            Name = "Bob",
                            NationalId = "547289",
                            PassportNum = "63546844"
                        },
                        new
                        {
                            Id = 3,
                            Age = 27,
                            FlightId = 2,
                            Gender = 1,
                            IsChild = false,
                            Name = "Alice",
                            NationalId = "223456",
                            PassportNum = "48567234"
                        },
                        new
                        {
                            Id = 4,
                            Age = 12,
                            FlightId = 2,
                            Gender = 0,
                            IsChild = true,
                            Name = "Charlie",
                            NationalId = "567890",
                            PassportNum = "58694230"
                        },
                        new
                        {
                            Id = 5,
                            Age = 45,
                            FlightId = 3,
                            Gender = 1,
                            IsChild = false,
                            Name = "Diana",
                            NationalId = "987654",
                            PassportNum = "11223344"
                        },
                        new
                        {
                            Id = 6,
                            Age = 26,
                            FlightId = 3,
                            Gender = 0,
                            IsChild = false,
                            Name = "Omar",
                            NationalId = "98322",
                            PassportNum = "234231"
                        });
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Plane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Engine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FlightId")
                        .HasColumnType("int");

                    b.Property<float?>("Height")
                        .HasColumnType("real");

                    b.Property<float?>("Length")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("WingSpan")
                        .HasColumnType("real");

                    b.Property<int?>("capacity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FlightId")
                        .IsUnique()
                        .HasFilter("[FlightId] IS NOT NULL");

                    b.ToTable("Planes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Engine = "CFM56-7B",
                            FlightId = 1,
                            Height = 41f,
                            Length = 110f,
                            Name = "Boeing 737",
                            WingSpan = 117f,
                            capacity = 188
                        },
                        new
                        {
                            Id = 2,
                            Engine = "CFM56-5B4",
                            FlightId = 2,
                            Height = 39f,
                            Length = 123f,
                            Name = "Airbus A320",
                            WingSpan = 117f,
                            capacity = 180
                        },
                        new
                        {
                            Id = 3,
                            Engine = "GE90-115B",
                            FlightId = 3,
                            Height = 61f,
                            Length = 242f,
                            Name = "Boeing 777",
                            WingSpan = 199f,
                            capacity = 396
                        });
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Number")
                        .HasColumnType("int");

                    b.Property<int?>("Section")
                        .HasColumnType("int");

                    b.Property<int?>("TicketId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TicketId")
                        .IsUnique()
                        .HasFilter("[TicketId] IS NOT NULL");

                    b.ToTable("Seats");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Number = 1,
                            Section = 4,
                            TicketId = 1
                        },
                        new
                        {
                            Id = 2,
                            Number = 2,
                            Section = 0,
                            TicketId = 2
                        },
                        new
                        {
                            Id = 3,
                            Number = 3,
                            Section = 1,
                            TicketId = 3
                        },
                        new
                        {
                            Id = 4,
                            Number = 4,
                            Section = 6,
                            TicketId = 4
                        },
                        new
                        {
                            Id = 5,
                            Number = 5,
                            Section = 2,
                            TicketId = 5
                        });
                });

            modelBuilder.Entity("Flight_Booking_System.Models.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AirPortId")
                        .HasColumnType("int");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AirPortId")
                        .IsUnique()
                        .HasFilter("[AirPortId] IS NOT NULL");

                    b.HasIndex("CountryId");

                    b.ToTable("States");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AirPortId = 1,
                            CountryId = 1,
                            Name = "Cairo"
                        },
                        new
                        {
                            Id = 2,
                            AirPortId = 2,
                            CountryId = 2,
                            Name = "Manhaten"
                        },
                        new
                        {
                            Id = 3,
                            AirPortId = 3,
                            CountryId = 1,
                            Name = "Aswan"
                        },
                        new
                        {
                            Id = 4,
                            AirPortId = 4,
                            CountryId = 4,
                            Name = "Sedney"
                        },
                        new
                        {
                            Id = 5,
                            AirPortId = 5,
                            CountryId = 5,
                            Name = "Tokyo"
                        });
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<int?>("FlightId")
                        .HasColumnType("int");

                    b.Property<int?>("PassengerId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.HasIndex("PassengerId")
                        .IsUnique()
                        .HasFilter("[PassengerId] IS NOT NULL");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Class = 0,
                            FlightId = 1,
                            PassengerId = 1,
                            Price = 299.99m
                        },
                        new
                        {
                            Id = 2,
                            Class = 1,
                            FlightId = 1,
                            PassengerId = 2,
                            Price = 499.99m
                        },
                        new
                        {
                            Id = 3,
                            Class = 2,
                            FlightId = 2,
                            PassengerId = 3,
                            Price = 1299.99m
                        },
                        new
                        {
                            Id = 4,
                            Class = 0,
                            FlightId = 2,
                            PassengerId = 4,
                            Price = 350.00m
                        },
                        new
                        {
                            Id = 5,
                            Class = 1,
                            FlightId = 3,
                            PassengerId = 5,
                            Price = 850.00m
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "2",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Flight_Booking_System.Models.ApplicationUSer", b =>
                {
                    b.HasOne("Flight_Booking_System.Models.Passenger", "Passenger")
                        .WithOne("User")
                        .HasForeignKey("Flight_Booking_System.Models.ApplicationUSer", "PassengerId");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Country", b =>
                {
                    b.HasOne("Flight_Booking_System.Models.AirPort", "AirPort")
                        .WithOne("Country")
                        .HasForeignKey("Flight_Booking_System.Models.Country", "AirPortId");

                    b.Navigation("AirPort");
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Flight", b =>
                {
                    b.HasOne("Flight_Booking_System.Models.AirPort", "DestinationAirport")
                        .WithMany("ArrivingFlights")
                        .HasForeignKey("DestinationAirportId");

                    b.HasOne("Flight_Booking_System.Models.AirPort", "SourceAirport")
                        .WithMany("LeavingFlights")
                        .HasForeignKey("SourceAirportId");

                    b.Navigation("DestinationAirport");

                    b.Navigation("SourceAirport");
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Passenger", b =>
                {
                    b.HasOne("Flight_Booking_System.Models.Flight", "Flight")
                        .WithMany("Passengers")
                        .HasForeignKey("FlightId");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Plane", b =>
                {
                    b.HasOne("Flight_Booking_System.Models.Flight", "Flight")
                        .WithOne("Plane")
                        .HasForeignKey("Flight_Booking_System.Models.Plane", "FlightId");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Seat", b =>
                {
                    b.HasOne("Flight_Booking_System.Models.Ticket", "Ticket")
                        .WithOne("Seat")
                        .HasForeignKey("Flight_Booking_System.Models.Seat", "TicketId");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Flight_Booking_System.Models.State", b =>
                {
                    b.HasOne("Flight_Booking_System.Models.AirPort", "AirPort")
                        .WithOne("State")
                        .HasForeignKey("Flight_Booking_System.Models.State", "AirPortId");

                    b.HasOne("Flight_Booking_System.Models.Country", "Country")
                        .WithMany("States")
                        .HasForeignKey("CountryId");

                    b.Navigation("AirPort");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Ticket", b =>
                {
                    b.HasOne("Flight_Booking_System.Models.Flight", "Flight")
                        .WithMany("Tickets")
                        .HasForeignKey("FlightId");

                    b.HasOne("Flight_Booking_System.Models.Passenger", "Passenger")
                        .WithOne("Ticket")
                        .HasForeignKey("Flight_Booking_System.Models.Ticket", "PassengerId");

                    b.Navigation("Flight");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Flight_Booking_System.Models.ApplicationUSer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Flight_Booking_System.Models.ApplicationUSer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Flight_Booking_System.Models.ApplicationUSer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Flight_Booking_System.Models.ApplicationUSer", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Flight_Booking_System.Models.AirPort", b =>
                {
                    b.Navigation("ArrivingFlights");

                    b.Navigation("Country");

                    b.Navigation("LeavingFlights");

                    b.Navigation("State");
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Country", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Flight", b =>
                {
                    b.Navigation("Passengers");

                    b.Navigation("Plane");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Passenger", b =>
                {
                    b.Navigation("Ticket");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Flight_Booking_System.Models.Ticket", b =>
                {
                    b.Navigation("Seat");
                });
#pragma warning restore 612, 618
        }
    }
}
