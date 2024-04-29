using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking_System.Repositories
{
    public class PlaceRepository : Repository<Place>, IPlaceRepository
    {
        public PlaceRepository(ITIContext _context) : base(_context)
        {

        }

        public void Insert(Place item)
        {
            Context.Add(item);
        }

        public void Update(Place item)
        {
            Context.Update(item);
        }

        public List<Place> GetAll(string? include = null)
        {
            if (include == null)
            {
                return Context.Places.ToList();
            }
            return Context.Places.Include(include).ToList();
        }

        public Place GetById(int Id)
        {
            return Context.Places.FirstOrDefault(item => item.Id == Id);
        }
        public List<Place> Get(Func<Place, bool> where)
        {
            return Context.Places.Where(where).ToList();
        }

        public void Delete(Place item)
        {
            Context.Remove(item);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
