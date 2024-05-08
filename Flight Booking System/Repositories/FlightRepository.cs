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

        public Flight? GetWithPlane(int id)
        {
            return Context.Flights
                 .Where(f => f.Id == id).Include(f => f.Plane).FirstOrDefault();
        }
    }
}
