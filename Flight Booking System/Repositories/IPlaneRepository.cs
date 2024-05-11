using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface IPlaneRepository : IRepository<Plane>
    {
        public Plane GetByFlightId(int Id);


    }
}
