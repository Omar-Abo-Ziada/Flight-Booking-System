using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Flight_Booking_System.Repositories
{

    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        public FlightRepository(ITIContext _context) : base(_context)
        {

        }

        //*************************************************************

        // used in system initialize
        public List<Flight> GetAllWithAllIncludes()
        {
          return  Context.Flights
                .Include(f => f.SourceAirport)
                .Include(f => f.DestinationAirport)
                .Include(f => f.Plane)
                .Include(f => f.Passengers)
                .Include(f => f.Tickets)
                .ToList();
        }

        public Flight? GetWithPlane_Passengers(int? id)
        {
            return Context.Flights
                 .Where(f => f.Id == id).Include(f => f.Plane).Include(f => f.Passengers).FirstOrDefault();
        }

        public Flight? GetWithTickets_Passengers(int? id)
        {
            return Context.Flights.Where(f => f.Id == id)
                .Include(f => f.Tickets)
                .Include(f => f.Passengers)
                .FirstOrDefault();
        }

        public Flight? GetWithTickets(int? id)
        {
            return Context.Flights.Where(f => f.Id == id)
                .Include(f => f.Tickets)
                .FirstOrDefault();
        }
    }
}
