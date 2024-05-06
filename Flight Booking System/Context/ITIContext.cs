using Flight_Booking_System.Enums;
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

        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Plane> Planes { get; set; }

        public DbSet<Passenger> Passengers { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Seat> Seats { get; set; }

        //hello
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>()
             .HasKey(l => new { l.LoginProvider, l.ProviderKey });


            modelBuilder.Entity<Flight>()
                   .HasOne(f => f.SourceAirport)
                   .WithMany(a => a.LeavingFlights)
                   .HasForeignKey(f => f.SourceAirportId)
                   .IsRequired(false);

            modelBuilder.Entity<Flight>()
                  .HasOne(f => f.DestinationAirport)
                  .WithMany(a => a.ArrivingFlights)
                  .HasForeignKey(f => f.DestinationAirportId)
                  .IsRequired(false);

            modelBuilder.Entity<IdentityRole>().HasData(
             new IdentityRole()
             {
                 Id = "1",
                 Name = "Admin",
                 NormalizedName = "ADMIN"
             },
              new IdentityRole()
              {
                  Id = "2",
                  Name = "User",
                  NormalizedName = "USER"
              }
             );


            #region Initial Data

            modelBuilder.Entity<AirPort>().HasData(
             new AirPort { Id = 1, Name = "Cairo AirPort", AirPortNumber = 1 },
             new AirPort { Id = 2, Name = "Frankfurt AirPort", AirPortNumber = 2 },
             new AirPort { Id = 3, Name = "Sydney AirPort", AirPortNumber = 3 },
             new AirPort { Id = 4, Name = "Dubai AirPort", AirPortNumber = 4 },
             new AirPort { Id = 5, Name = "Atlanta AirPort", AirPortNumber = 5 }
         );

            modelBuilder.Entity<Country>().HasData(
             new Country { Id = 1, Name = "Egypt", AirPortId = 1 },
             new Country { Id = 2, Name = "USA", AirPortId = 2 },
             new Country { Id = 3, Name = "Germany", AirPortId = 3 },
             new Country { Id = 4, Name = "Australia", AirPortId = 4 },
             new Country { Id = 5, Name = "Japan", AirPortId = 5 }
         );

            modelBuilder.Entity<State>().HasData(
               new State { Id = 1, Name = "Cairo", CountryId = 1, AirPortId = 1 },
               new State { Id = 2, Name = "Manhaten", CountryId = 2, AirPortId = 2 },
               new State { Id = 3, Name = "Aswan", CountryId = 1, AirPortId = 3 },
               new State { Id = 4, Name = "Sedney", CountryId = 4, AirPortId = 4 },
               new State { Id = 5, Name = "Tokyo", CountryId = 5, AirPortId = 5 }
          );

            modelBuilder.Entity<Flight>().HasData(
            new List<Flight>
            {
                    new Flight
                    {
                        Id = 1,
                        SourceAirportId = 1,
                        DestinationAirportId = 2,
                        DepartureTime = new DateTime(2024, 12, 25, 10, 30, 0),
                        ArrivalTime = new DateTime(2024, 12, 25, 16, 45, 0),
                        Duration = new TimeSpan(6, 15, 0),
                    },
                    new Flight
                    {
                        Id = 2,
                        SourceAirportId = 2,
                        DestinationAirportId = 1,
                        DepartureTime = new DateTime(2024, 12, 30, 14, 00, 0),
                        ArrivalTime = new DateTime(2024, 12, 30, 20, 00, 0),
                        Duration = new TimeSpan(6, 0, 0),
                    },
                    new Flight
                    {
                        Id = 3,
                        SourceAirportId = 3,
                        DestinationAirportId = 1,
                        DepartureTime = new DateTime(2024, 11, 15, 9, 0, 0),
                        ArrivalTime = new DateTime(2024, 11, 15, 12, 15, 0),
                        Duration = new TimeSpan(3, 15, 0),
                    }
                }
            );

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
                        FlightId = 1 ,
                        //SeatId = 1 ,
                    },
                    new Ticket
                    {
                        Id = 2,
                        Price = 499.99m,
                        Class = Class.Business,
                        PassengerId = 2,
                        FlightId = 1 ,
                        //SeatId = 2 ,
                    },
                    new Ticket
                    {
                        Id = 3,
                        Price = 1299.99m,
                        Class = Class.First,
                        PassengerId = 3,
                        FlightId = 2 ,
                        //SeatId = 3 ,
                    },
                    new Ticket
                    {
                        Id = 4,
                        Price = 350.00m,
                        Class = Class.Economy,
                        PassengerId = 4,
                        FlightId = 2 ,
                        //SeatId = 4 ,
                    },
                    new Ticket
                    {
                        Id = 5,
                        Price = 850.00m,
                        Class = Class.Business,
                        PassengerId = 5,
                        FlightId = 3 ,
                        //SeatId = 5 ,
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
                     FlightId = 1,
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
                     FlightId = 1,
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
                     FlightId = 2,
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
                     FlightId = 2,
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
                     FlightId = 3,
                     //TicketId = 5
                 },
                new Passenger
                {
                    Id = 6,
                    Name = "Omar",
                    Gender = Gender.Male,
                    IsChild = false,
                    Age = 26,
                    NationalId = "98322",
                    PassportNum = "234231",
                    FlightId = 3,
                    //TicketId = 6
                }
             );

            #endregion

        }
    }
}
