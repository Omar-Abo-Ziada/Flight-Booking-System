using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface IFlightRepository : IRepository<Flight>
    {
        Flight? GetWithPlane_Passengers(int? id);

        List<Flight> GetAllWithAllIncludes();

        Flight? GetWithTickets_Passengers(int? id);

    }
}
