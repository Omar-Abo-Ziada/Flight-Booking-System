using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking_System.Repositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(ITIContext _context) : base(_context)
        {
        }

        public void Insert(Ticket item)
        {
            Context.Add(item);
        }

        public void Update(Ticket item)
        {
            Context.Update(item);
        }

        public List<Ticket> GetAll(string? include = null)
        {
            if (include == null)
            {
                return Context.Tickets.ToList();
            }
            return Context.Tickets.Include(include).ToList();
        }

        public Ticket GetById(int Id)
        {
            return Context.Tickets.FirstOrDefault(item => item.Id == Id);
        }
        public List<Ticket> Get(Func<Ticket, bool> where)
        {
            return Context.Tickets.Where(where).ToList();
        }

        public void Delete(Ticket item)
        {
            Context.Remove(item);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
