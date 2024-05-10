using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface IPassengerRepository : IRepository<Passenger>
    {
        Passenger? GetWithTicket(int? id);
    }
}
