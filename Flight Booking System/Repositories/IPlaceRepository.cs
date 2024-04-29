using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface IPlaceRepository : IRepository<Place>
    {
        List<Place> GetAll(string? include = null);

        Place GetById(int id);

        List<Place> Get(Func<Place, bool> where);

        void Insert(Place item);

        void Update(Place item);

        void Delete(Place item);

        void Save();
    }
}
