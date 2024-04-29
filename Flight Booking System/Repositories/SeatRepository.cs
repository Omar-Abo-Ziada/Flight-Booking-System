using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking_System.Repositories
{
    public class SeatRepository : Repository<Seat>, ISeatRepository
    {
        public SeatRepository(ITIContext _context) : base(_context)
        {
        }

        public void Insert(Seat item)
        {
            Context.Add(item);
        }

        public void Update(Seat item)
        {
            Context.Update(item);
        }

        public List<Seat> GetAll(string? include = null)
        {
            if (include == null)
            {
                return Context.Seats.ToList();
            }
            return Context.Seats.Include(include).ToList();
        }

        public Seat GetById(int Id)
        {
            return Context.Seats.FirstOrDefault(item => item.Id == Id);
        }
        public List<Seat> Get(Func<Seat, bool> where)
        {
            return Context.Seats.Where(where).ToList();
        }

        public void Delete(Seat item)
        {
            Context.Remove(item);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
