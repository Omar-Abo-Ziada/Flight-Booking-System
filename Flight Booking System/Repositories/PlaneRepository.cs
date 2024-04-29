using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking_System.Repositories
{
    public class PlaneRepository : Repository<Plane>, IPlaneRepository
    {
        public PlaneRepository(ITIContext _context) : base(_context)
        {
        }

        public void Insert(Plane item)
        {
            Context.Add(item);
        }

        public void Update(Plane item)
        {
            Context.Update(item);
        }

        public List<Plane> GetAll(string? include = null)
        {
            if (include == null)
            {
                return Context.Planes.ToList();
            }
            return Context.Planes.Include(include).ToList();
        }

        public Plane GetById(int Id)
        {
            return Context.Planes.FirstOrDefault(item => item.Id == Id);
        }
        public List<Plane> Get(Func<Plane, bool> where)
        {
            return Context.Planes.Where(where).ToList();
        }

        public void Delete(Plane item)
        {
            Context.Remove(item);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
