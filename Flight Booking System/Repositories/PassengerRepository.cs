using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Flight_Booking_System.Repositories
{

    public class PassengerRepository : Repository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(ITIContext _context) : base(_context)
        {

        }

        //*************************************************

    }
}
