using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking_System.Context
{
    public class ITIContext : DbContext
    {
        public DbSet<AirPort> AirPort { get; set; }

        public DbSet<AirLine> AirLines { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Plane> Planes { get; set; }

        public DbSet<Passenger> Passengers { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public ITIContext(DbContextOptions contextOptions) : base(contextOptions) 
        {
            
        }
    }
}
