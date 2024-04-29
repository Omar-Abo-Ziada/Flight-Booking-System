using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface IPlaneRepository : IRepository<Plane>
    {
        List<Plane> GetAll(string? include = null);

        Plane GetById(int id);

        List<Plane> Get(Func<Plane, bool> where);

        void Insert(Plane item);

        void Update(Plane item);

        void Delete(Plane item);

        void Save();
    }
}
