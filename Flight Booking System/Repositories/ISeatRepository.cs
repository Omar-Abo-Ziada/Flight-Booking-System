using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface ISeatRepository : IRepository<Seat>
    {
         Seat? GetWithTicket(int? id);
    }
}
