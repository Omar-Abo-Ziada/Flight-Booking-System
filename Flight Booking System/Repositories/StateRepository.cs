using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking_System.Repositories
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        public StateRepository(ITIContext _context) : base(_context)
        {

        }

        //*************************************************

    }
}
