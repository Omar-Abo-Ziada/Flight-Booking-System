using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface IFlightRepository : IRepository<Flight>
    {
        Flight? GetWithPlane_Passengers(int? id);

        List<Flight> GetAllWithAllIncludes();
        
        public Flight? GetWitPassengers_Tickets(int? id);
        
        public Flight? GetOneWithAllIncludes(int id);

        Flight? GetWithTickets_Passengers(int? id);

        Flight? GetWithTickets(int? id);
    }
}
