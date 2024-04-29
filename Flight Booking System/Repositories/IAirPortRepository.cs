using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface IAirPortRepository : IRepository<AirPort>
    {
        List<AirPort> GetAll(string? include = null);

        AirPort GetById(int id);

        List<AirPort> Get(Func<AirPort, bool> where);

        void Insert(AirPort item);

        void Update(AirPort item);

        void Delete(AirPort item);

        void Save();
    }
}
