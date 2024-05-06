using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking_System.Repositories
{
    public class AirPortRepository : Repository<AirPort>, IAirPortRepository
    {
        public AirPortRepository(ITIContext _context) : base(_context)
        {

        }

        //**************************************************************

        /// TODO : use this when u want to include country and state with the airport
        //public List<Place> GetAllWithChilds(int flightId)
        //{
        //    Flight flightfromDB = Context.Flights.FirstOrDefault(f => f.Id == flightId);

        //    IQueryable<Place> query = Context.Places.Where(p => p.ArrivingFlights.Contains(flightfromDB));

        //    query = query.Include(p => p.Country).Include(p => p.State);

        //    return query.ToList();
        //}

    }
}
