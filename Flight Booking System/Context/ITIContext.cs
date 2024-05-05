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

            modelBuilder.Entity<AirLine>().HasData(
                new AirLine { Id = 1, AirlineNumber = 5, Name = "EgyptAirs", AirportId = 1 },
                new AirLine { Id = 2, AirlineNumber = 10, Name = "Lufthansa", AirportId = 2 },
                new AirLine { Id = 3, AirlineNumber = 15, Name = "Qantas", AirportId = 3 },
                new AirLine { Id = 4, AirlineNumber = 20, Name = "Emirates", AirportId = 4 },
                new AirLine { Id = 5, AirlineNumber = 25, Name = "Delta", AirportId = 5 }
        );


            modelBuilder.Entity<AirPort>().HasData(
                new AirPort { Id = 1, Name = "Cairo AirPort", AirPortNumber = 1 },
                new AirPort { Id = 2, Name = "Frankfurt AirPort", AirPortNumber = 2 },
                new AirPort { Id = 3, Name = "Sydney AirPort", AirPortNumber = 3 },
                new AirPort { Id = 4, Name = "Dubai AirPort", AirPortNumber = 4 },
                new AirPort { Id = 5, Name = "Atlanta AirPort", AirPortNumber = 5 }
            );

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
                     TicketId = 1
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
                     TicketId = 2
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
                     TicketId = 3
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
                     TicketId = 4
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
                     TicketId = 5
                 }
             );
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Egypt" },
                new Country { Id = 2, Name = "USA" },
                new Country { Id = 3, Name = "Germany" },
                new Country { Id = 4, Name = "Australia" },
                new Country { Id = 5, Name = "Japan" }
            );
            modelBuilder.Entity<Place>().HasData(
               new Place { Id = 1, CountryId = 1, StateId = 1 },
               new Place { Id = 2, CountryId = 1, StateId = 2 },
               new Place { Id = 3, CountryId = 1, StateId = 3 },   // saeed : change country id values >> to make sense
               new Place { Id = 4, CountryId = 2, StateId = 4 },
               new Place { Id = 5, CountryId = 2, StateId = 5 }
           );
            modelBuilder.Entity<Plane>().HasData(
     new List<Plane>
     {
        new Plane
        {
            Id = 1,
            Name = "Boeing 737",
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
            capacity = 396,
            Engine = "GE90-115B",
            Height = 61,
            Length = 242,
            WingSpan = 199
        },
        new Plane
        {
            Id = 4,
            Name = "Airbus A380",
            capacity = 853,
            Engine = "Trent 900",
            Height = 79,
            Length = 238,
            WingSpan = 261
        }
     }
 );
            modelBuilder.Entity<State>().HasData(
                new State { Id = 1, CountryId = 1, Name = "Cairo" },
                new State { Id = 2, CountryId = 1, Name = "Alexandria" },
                new State { Id = 3, CountryId = 1, Name = "Aswan" },
                new State { Id = 4, CountryId = 2, Name = "Texas" },
                new State { Id = 5, CountryId = 2, Name = "California" }
                );
            modelBuilder.Entity<Seat>().HasData(
                new List<Seat>
                {
                    new Seat { Id = 1, Number = 1, Section = Section.front | Section.window },
                    new Seat { Id = 2, Number = 2, Section = Section.front },
                    new Seat { Id = 3, Number = 3, Section = Section.Middle },
                    new Seat { Id = 4, Number = 4, Section = Section.back | Section.window },
                    new Seat { Id = 5, Number = 5, Section = Section.back },
                    new Seat { Id = 6, Number = 6, Section = Section.Middle | Section.window },
                    new Seat { Id = 7, Number = 7, Section = Section.front | Section.window },
                    new Seat { Id = 8, Number = 8, Section = Section.back },
                    new Seat { Id = 9, Number = 9, Section = Section.Middle },
                    new Seat { Id = 10, Number = 10, Section = Section.front | Section.window }
                }
            );

            modelBuilder.Entity<Flight>().HasData(
     new List<Flight>
     {
        new Flight
        {
            Id = 1,
            StartId = 1,
            DestinationId = 1,
            DepartureTime = new DateTime(2024, 12, 25, 10, 30, 0),
            ArrivalTime = new DateTime(2024, 12, 25, 16, 45, 0),
            Duration = new TimeSpan(6, 15, 0),
            PlaneId = 1
        },
        new Flight
        {
            Id = 2,
            StartId = 2,
            DestinationId = 2,
            DepartureTime = new DateTime(2024, 12, 30, 14, 00, 0),
            ArrivalTime = new DateTime(2024, 12, 30, 20, 00, 0),
            Duration = new TimeSpan(6, 0, 0),
            PlaneId = 2
        },
        new Flight
        {
            Id = 3,
            StartId = 3,
            DestinationId = 3,
            DepartureTime = new DateTime(2024, 11, 15, 9, 0, 0),
            ArrivalTime = new DateTime(2024, 11, 15, 12, 15, 0),
            Duration = new TimeSpan(3, 15, 0),
            PlaneId = 3
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
            SeatId = 1,
            FlightId = 1
        },
        new Ticket
        {
            Id = 2,
            Price = 499.99m,
            Class = Class.Business,
            PassengerId = 2,
            SeatId = 2,
            FlightId = 1
        },
        new Ticket
        {
            Id = 3,
            Price = 1299.99m,
            Class = Class.First,
            PassengerId = 3,
            SeatId = 3,
            FlightId = 2
        },
        new Ticket
        {
            Id = 4,
            Price = 350.00m,
            Class = Class.Economy,
            PassengerId = 4,
            SeatId = 4,
            FlightId = 2
        },
        new Ticket
        {
            Id = 5,
            Price = 850.00m,
            Class = Class.Business,
            PassengerId = 5,
            SeatId = 5,
            FlightId = 3
        }
    }
);

            //adding extra info ibrahim   


            // Adding more airlines
            modelBuilder.Entity<AirLine>().HasData(
                new AirLine { Id = 6, AirlineNumber = 30, Name = "British Airways", AirportId = 6 },
                new AirLine { Id = 7, AirlineNumber = 35, Name = "Air France", AirportId = 7 },
                new AirLine { Id = 8, AirlineNumber = 40, Name = "American Airlines", AirportId = 8 },
                new AirLine { Id = 9, AirlineNumber = 45, Name = "United Airlines", AirportId = 9 },
                new AirLine { Id = 10, AirlineNumber = 50, Name = "Singapore Airlines", AirportId = 10 }

            );

            // Adding more airports
            modelBuilder.Entity<AirPort>().HasData(
                new AirPort { Id = 6, Name = "Heathrow Airport", AirPortNumber = 6 },
                new AirPort { Id = 7, Name = "Charles de Gaulle Airport", AirPortNumber = 7 },
                new AirPort { Id = 8, Name = "Los Angeles International Airport", AirPortNumber = 8 },
                new AirPort { Id = 9, Name = "O'Hare International Airport", AirPortNumber = 9 },
                new AirPort { Id = 10, Name = "Changi Airport", AirPortNumber = 10 }

            );

            // Adding more passengers
            modelBuilder.Entity<Passenger>().HasData(
                new Passenger { Id = 6, Name = "John", Gender = Gender.Male, IsChild = false, Age = 35, NationalId = "789012", PassportNum = "77889900", TicketId = 6 },
                new Passenger { Id = 7, Name = "Sophia", Gender = Gender.Female, IsChild = false, Age = 29, NationalId = "567890", PassportNum = "11223344", TicketId = 7 },
                new Passenger { Id = 8, Name = "William", Gender = Gender.Male, IsChild = false, Age = 45, NationalId = "345678", PassportNum = "55667788", TicketId = 8 },
                new Passenger { Id = 9, Name = "Olivia", Gender = Gender.Female, IsChild = true, Age = 8, NationalId = "456789", PassportNum = "99001122", TicketId = 9 },
                new Passenger { Id = 10, Name = "James", Gender = Gender.Male, IsChild = false, Age = 55, NationalId = "234567", PassportNum = "33445566", TicketId = 10 }

            );

            // Adding more countries
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 6, Name = "United Kingdom" },
                new Country { Id = 7, Name = "France" },
                new Country { Id = 8, Name = "United States" },
                new Country { Id = 9, Name = "Singapore" },
                new Country { Id = 10, Name = "Qatar" }

            );

            // Adding more states
            modelBuilder.Entity<State>().HasData(
                new State { Id = 6, CountryId = 6, Name = "England" },
                new State { Id = 7, CountryId = 6, Name = "Scotland" },
                new State { Id = 8, CountryId = 6, Name = "Wales" },
                new State { Id = 9, CountryId = 7, Name = "Île-de-France" },
                new State { Id = 10, CountryId = 7, Name = "Provence-Alpes-Côte d'Azur" }

            );

            // Adding more planes
            modelBuilder.Entity<Plane>().HasData(
                new Plane { Id = 5, Name = "Boeing 787", capacity = 242, Engine = "GEnx-1B", Height = 56, Length = 206, WingSpan = 197 },
                new Plane { Id = 6, Name = "Airbus A350", capacity = 325, Engine = "Trent XWB", Height = 56, Length = 227, WingSpan = 212 },
                new Plane { Id = 7, Name = "Boeing 767", capacity = 375, Engine = "CF6-80", Height = 48, Length = 201, WingSpan = 156 },
                new Plane { Id = 8, Name = "Airbus A330", capacity = 440, Engine = "Trent 700", Height = 58, Length = 63, WingSpan = 197 },
                new Plane { Id = 9, Name = "Boeing 757", capacity = 295, Engine = "RB211-535", Height = 44, Length = 54, WingSpan = 38 }

            );

            // Adding more places
            modelBuilder.Entity<Place>().HasData(
                new Place { Id = 6, CountryId = 6, StateId = 6 },
                new Place { Id = 7, CountryId = 6, StateId = 7 },
                new Place { Id = 8, CountryId = 6, StateId = 8 },
                new Place { Id = 9, CountryId = 7, StateId = 9 },
                new Place { Id = 10, CountryId = 7, StateId = 10 }

            );

            // Adding more seats
            modelBuilder.Entity<Seat>().HasData(
                new Seat { Id = 11, Number = 11, Section = Section.front | Section.window },
                new Seat { Id = 12, Number = 12, Section = Section.front },
                new Seat { Id = 13, Number = 13, Section = Section.Middle },
                new Seat { Id = 14, Number = 14, Section = Section.back | Section.window },
                new Seat { Id = 15, Number = 15, Section = Section.back }

            );

            // Adding more flights
            modelBuilder.Entity<Flight>().HasData(
                new Flight { Id = 4, StartId = 4, DestinationId = 4, DepartureTime = new DateTime(2024, 12, 25, 10, 30, 0), ArrivalTime = new DateTime(2024, 12, 25, 16, 45, 0), Duration = new TimeSpan(6, 15, 0), PlaneId = 5 },
                new Flight { Id = 5, StartId = 5, DestinationId = 5, DepartureTime = new DateTime(2024, 12, 30, 14, 0, 0), ArrivalTime = new DateTime(2024, 12, 30, 20, 0, 0), Duration = new TimeSpan(6, 0, 0), PlaneId = 6 },
                new Flight { Id = 6, StartId = 6, DestinationId = 6, DepartureTime = new DateTime(2024, 11, 15, 9, 0, 0), ArrivalTime = new DateTime(2024, 11, 15, 12, 15, 0), Duration = new TimeSpan(3, 15, 0), PlaneId = 7 }

            );

            // Adding more tickets
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { Id = 6, Price = 399.99m, Class = Class.Economy, PassengerId = 6, SeatId = 11, FlightId = 4 },
                new Ticket { Id = 7, Price = 599.99m, Class = Class.Business, PassengerId = 7, SeatId = 12, FlightId = 5 },
                new Ticket { Id = 8, Price = 1399.99m, Class = Class.First, PassengerId = 8, SeatId = 13, FlightId = 6 },
                new Ticket { Id = 9, Price = 380.00m, Class = Class.Economy, PassengerId = 9, SeatId = 14, FlightId = 4 },
                new Ticket { Id = 10, Price = 900.00m, Class = Class.Business, PassengerId = 10, SeatId = 15, FlightId = 5 }

            );

        }
    }
}
