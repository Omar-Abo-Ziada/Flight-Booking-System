using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking_System.Repositories
{
    public class PlaneRepository : Repository<Plane>, IPlaneRepository
    {
        public PlaneRepository(ITIContext _context) : base(_context)
        {

        }

        //*************************************************

     
    }
}
