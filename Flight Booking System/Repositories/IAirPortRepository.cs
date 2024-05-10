using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface IAirPortRepository : IRepository<AirPort>
    {

        AirPort? GetWithIncludes(int? id);

        public AirPort? GetSourceWithFlights(int? id);

        public AirPort? GetDsetinationWithFlights(int? id);
    }
}
