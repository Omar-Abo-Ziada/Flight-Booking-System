using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking_System.Repositories
{
     public class AirPortRepository : Repository<AirPort>, IAirPortRepository
    {
        public AirPortRepository(ITIContext _context) : base(_context)
        {
        }

        public void Insert(AirPort item)
        {
            Context.Add(item);
        }

        public void Update(AirPort item)
        {
            Context.Update(item);
        }

        public List<AirPort> GetAll(string? include = null)
        {
            if (include == null)
            {
                return Context.AirPorts.ToList();
            }
            return Context.AirPorts.Include(include).ToList();
        }

        public AirPort GetById(int Id)
        {
            return Context.AirPorts.FirstOrDefault(item => item.Id == Id);
        }
        public List<AirPort> Get(Func<AirPort, bool> where)
        {
            return Context.AirPorts.Where(where).ToList();
        }

        public void Delete(AirPort item)
        {
            Context.Remove(item);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
