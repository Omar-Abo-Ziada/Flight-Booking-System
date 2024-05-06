﻿using Flight_Booking_System.Enums;
using Flight_Booking_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking_System.Context
{
    public class ITIContext : IdentityDbContext<ApplicationUSer> // DbContext
    {
        public ITIContext(DbContextOptions<ITIContext> options) : base(options)
        {

        }

        public DbSet<AirPort> AirPorts { get; set; }

        public DbSet<AirLine> AirLines { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Plane> Planes { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Passenger> Passengers { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Seat> Seats { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>()
             .HasKey(l => new { l.LoginProvider, l.ProviderKey });


            modelBuilder.Entity<Flight>()
                   .HasOne(f => f.Start)
                   .WithMany(p => p.DepartingFlights)
                   .HasForeignKey(f => f.StartId)
                   .IsRequired(false);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Destination)
                .WithMany(p => p.ArrivingFlights)
                .HasForeignKey(f => f.DestinationId)
                .IsRequired(false);


            #region Initial Data

            modelBuilder.Entity<AirPort>().HasData(
             new AirPort { Id = 1, Name = "Cairo AirPort", AirPortNumber = 1 },
             new AirPort { Id = 2, Name = "Frankfurt AirPort", AirPortNumber = 2 },
             new AirPort { Id = 3, Name = "Sydney AirPort", AirPortNumber = 3 },
             new AirPort { Id = 4, Name = "Dubai AirPort", AirPortNumber = 4 },
             new AirPort { Id = 5, Name = "Atlanta AirPort", AirPortNumber = 5 }
         );


            modelBuilder.Entity<AirLine>().HasData(
                new AirLine { Id = 1, AirlineNumber = 5, Name = "EgyptAirs", AirportId = 1 },
                new AirLine { Id = 2, AirlineNumber = 10, Name = "Lufthansa", AirportId = 2 },
                new AirLine { Id = 3, AirlineNumber = 15, Name = "Qantas", AirportId = 3 },
                new AirLine { Id = 4, AirlineNumber = 20, Name = "Emirates", AirportId = 4 },
                new AirLine { Id = 5, AirlineNumber = 25, Name = "Delta", AirportId = 5 }
        );


            modelBuilder.Entity<Passenger>().HasData(
                 new Passenger
                 {
                     Id = 1,
                     Name = "Joy",
                     Gender = Gender.Female,
                     IsChild = false,
                     Age = 22,
                     NationalId = "302245",
                     PassportNum = "52546874",
                     //TicketId = 1
                 },
                 new Passenger
                 {
                     Id = 2,
                     Name = "Bob",
                     Gender = Gender.Male,
                     IsChild = false,
                     Age = 30,
                     NationalId = "547289",
                     PassportNum = "63546844",
                     //TicketId = 2
                 },
                 new Passenger
                 {
                     Id = 3,
                     Name = "Alice",
                     Gender = Gender.Female,
                     IsChild = false,
                     Age = 27,
                     NationalId = "223456",
                     PassportNum = "48567234",
                     //TicketId = 3
                 },
                 new Passenger
                 {
                     Id = 4,
                     Name = "Charlie",
                     Gender = Gender.Male,
                     IsChild = true,
                     Age = 12,
                     NationalId = "567890",
                     PassportNum = "58694230",
                     //TicketId = 4
                 },
                 new Passenger
                 {
                     Id = 5,
                     Name = "Diana",
                     Gender = Gender.Female,
                     IsChild = false,
                     Age = 45,
                     NationalId = "987654",
                     PassportNum = "11223344",
                     //TicketId = 5
                 }
             );


            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Egypt", PlaceId = 1 },
                new Country { Id = 2, Name = "USA", PlaceId = 2 },
                new Country { Id = 3, Name = "Germany", PlaceId = 3 },
                new Country { Id = 4, Name = "Australia", PlaceId = 4 },
                new Country { Id = 5, Name = "Japan", PlaceId = 5 }
            );

            modelBuilder.Entity<State>().HasData(
           new State { Id = 1, CountryId = 1, Name = "Cairo", PlaceId = 1 },
           new State { Id = 2, CountryId = 2, Name = "Manhaten", PlaceId = 2 },
           new State { Id = 3, CountryId = 1, Name = "Aswan", PlaceId = 3 },
           new State { Id = 4, CountryId = 4, Name = "Sedney", PlaceId = 4 },
           new State { Id = 5, CountryId = 5, Name = "Tokyo", PlaceId = 5 }
           );

            modelBuilder.Entity<Place>().HasData(
              new Place { Id = 1 },
              new Place { Id = 2 },
              new Place { Id = 3 },   // saeed : change country id values >> to make sense
              new Place { Id = 4 },
              new Place { Id = 5 }
                );

            //         modelBuilder.Entity<Place>().HasData(
            //    new Place { Id = 1, CountryId = 1, StateId = 1, },
            //    new Place { Id = 2, CountryId = 1, StateId = 2 },
            //    new Place { Id = 3, CountryId = 1, StateId = 3 },   // saeed : change country id values >> to make sense
            //    new Place { Id = 4, CountryId = 2, StateId = 4 },
            //    new Place { Id = 5, CountryId = 2, StateId = 5 }
            //);



            modelBuilder.Entity<Plane>().HasData(
             new List<Plane>
             {
                    new Plane
                    {
                        Id = 1,
                        Name = "Boeing 737",
                        FlightId = 1,
                        capacity = 188,
                        Engine = "CFM56-7B",
                        Height = 41,
                        Length = 110,
                        WingSpan = 117
                    },
                    new Plane
                    {
                        Id = 2,
                        Name = "Airbus A320",
                        FlightId = 2,
                        capacity = 180,
                        Engine = "CFM56-5B4",
                        Height = 39 ,
                        Length = 123,
                        WingSpan = 117
                    },
                    new Plane
                    {
                        Id = 3,
                        Name = "Boeing 777",
                        FlightId = 3,
                        capacity = 396,
                        Engine = "GE90-115B",
                        Height = 61,
                        Length = 242,
                        WingSpan = 199
                    }
                }
            );

            modelBuilder.Entity<Ticket>().HasData(
                    new List<Ticket>
                    {
                    new Ticket
                    {
                        Id = 1,
                        Price = 299.99m,
                        Class = Class.Economy,
                        PassengerId = 1,
                        FlightId = 1
                    },
                    new Ticket
                    {
                        Id = 2,
                        Price = 499.99m,
                        Class = Class.Business,
                        PassengerId = 2,
                        FlightId = 1
                    },
                    new Ticket
                    {
                        Id = 3,
                        Price = 1299.99m,
                        Class = Class.First,
                        PassengerId = 3,
                        FlightId = 2
                    },
                    new Ticket
                    {
                        Id = 4,
                        Price = 350.00m,
                        Class = Class.Economy,
                        PassengerId = 4,
                        FlightId = 2
                    },
                    new Ticket
                    {
                        Id = 5,
                        Price = 850.00m,
                        Class = Class.Business,
                        PassengerId = 5,
                        FlightId = 3
                    }
                    }
                  
                    );


            modelBuilder.Entity<Seat>().HasData(
                new List<Seat>
                {
                    new Seat { Id = 1, Number = 1 , TicketId = 1 , Section = Section.front | Section.window },
                    new Seat { Id = 2, Number = 2,TicketId = 2 , Section = Section.front },
                    new Seat { Id = 3, Number = 3, TicketId = 3 ,Section = Section.Middle },
                    new Seat { Id = 4, Number = 4,TicketId = 4 , Section = Section.back | Section.window },
                    new Seat { Id = 5, Number = 5,TicketId = 5 , Section = Section.back },
                    //new Seat { Id = 6, Number = 6,TicketId = 1 , Section = Section.Middle | Section.window },
                    //new Seat { Id = 7, Number = 7,TicketId = 2 , Section = Section.front | Section.window },
                    //new Seat { Id = 8, Number = 8,TicketId = 3 , Section = Section.back },
                    //new Seat { Id = 9, Number = 9,TicketId = 4 , Section = Section.Middle },
                    //new Seat { Id = 10, Number = 10,TicketId = 5 , Section = Section.front | Section.window }
                }
            );


            modelBuilder.Entity<Flight>().HasData(
             new List<Flight>
             {
                    new Flight
                    {
                        Id = 1,
                        StartId = 1,
                        DestinationId = 2,
                        DepartureTime = new DateTime(2024, 12, 25, 10, 30, 0),
                        ArrivalTime = new DateTime(2024, 12, 25, 16, 45, 0),
                        Duration = new TimeSpan(6, 15, 0),
                        AirLineId = 1 
                    },
                    new Flight
                    {
                        Id = 2,
                        StartId = 2,
                        DestinationId = 1,
                        DepartureTime = new DateTime(2024, 12, 30, 14, 00, 0),
                        ArrivalTime = new DateTime(2024, 12, 30, 20, 00, 0),
                        Duration = new TimeSpan(6, 0, 0),
                        AirLineId = 2
                    },
                    new Flight
                    {
                        Id = 3,
                        StartId = 3,
                        DestinationId = 1,
                        DepartureTime = new DateTime(2024, 11, 15, 9, 0, 0),
                        ArrivalTime = new DateTime(2024, 11, 15, 12, 15, 0),
                        Duration = new TimeSpan(3, 15, 0),
                        AirLineId = 3
                    }
                 }
             );

            #endregion

        }
    }
}
