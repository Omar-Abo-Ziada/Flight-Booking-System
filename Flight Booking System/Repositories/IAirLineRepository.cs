using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface IAirLineRepository : IRepository<AirLine>
    {
        List<AirLine> GetAll(string? include = null);

        AirLine GetById(int id);

        List<AirLine> Get(Func<AirLine, bool> where);

        void Insert(AirLine item);

        void Update(AirLine item);

        void Delete(AirLine item);

        void Save();
    }

    }
