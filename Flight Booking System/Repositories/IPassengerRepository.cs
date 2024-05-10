using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface IPassengerRepository : IRepository<Passenger>
    {

        List<Passenger>? GetPassengersForFlightWithAllIncludes(int flightId);

        Passenger? GetWithTicket(int? id);
    }
}
