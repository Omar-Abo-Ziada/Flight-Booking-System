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

            //modelBuilder.Entity<Flight>()
            //   .HasOne(f => f.Start)
            //   .WithMany(p => p.DepartingFlights)
            //   .HasForeignKey(f => f.StartId)
            //   .IsRequired(false);

            //modelBuilder.Entity<Flight>()
            //    .HasOne(f => f.Destination)
            //    .WithMany(p => p.ArrivingFlights)
            //    .HasForeignKey(f => f.DestinationId)
            //    .IsRequired(false);

        }
    }
}
