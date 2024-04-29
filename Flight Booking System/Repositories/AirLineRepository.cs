using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking_System.Repositories
{
    public class AirLineRepository : Repository<AirLine>, IAirLineRepository
    {
        public AirLineRepository(ITIContext _context) : base(_context)
        {
        }

        public void Insert(AirLine item)
        {
            Context.Add(item);
        }

        public void Update(AirLine item)
        {
            Context.Update(item);
        }

        public List<AirLine> GetAll(string? include = null)
        {
            if (include == null)
            {
                return Context.AirLines.ToList();
            }
            return Context.AirLines.Include(include).ToList();
        }

        public AirLine GetById(int Id)
        {
            return Context.AirLines.FirstOrDefault(item => item.Id == Id);
        }
        public List<AirLine> Get(Func<AirLine, bool> where)
        {
            return Context.AirLines.Where(where).ToList();
        }

        public void Delete(AirLine item)
        {
            Context.Remove(item);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
