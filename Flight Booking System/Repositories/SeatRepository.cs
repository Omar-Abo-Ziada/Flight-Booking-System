using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking_System.Repositories
{
    public class SeatRepository : Repository<Seat>, ISeatRepository
    {
        public SeatRepository(ITIContext _context) : base(_context)
        {

        }

        //*******************************************

        public Seat? GetWithTicket(int? id)
        {
            return Context.Seats.Where(s => s.Id == id).FirstOrDefault();
        }
    }
}
