using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface ISeatRepository : IRepository<Seat>
    {
        List<Seat> GetAll(string? include = null);

        Seat GetById(int id);

        List<Seat> Get(Func<Seat, bool> where);

        void Insert(Seat item);

        void Update(Seat item);

        void Delete(Seat item);

        void Save();
    }
}
