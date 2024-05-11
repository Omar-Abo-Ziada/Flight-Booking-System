using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Ticket? GetWithSeat_Passenger(int? id);
        public List<Ticket> GetWithSeatByFlightId(int id);

    }
}
