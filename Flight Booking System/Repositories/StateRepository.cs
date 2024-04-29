using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking_System.Repositories
{
    public class StateRepository : Repository<State>, IStateRepository
    {
        public StateRepository(ITIContext _context) : base(_context)
        {
        }

        public void Insert(State item)
        {
            Context.Add(item);
        }

        public void Update(State item)
        {
            Context.Update(item);
        }

        public List<State> GetAll(string? include = null)
        {
            if (include == null)
            {
                return Context.States.ToList();
            }
            return Context.States.Include(include).ToList();
        }

        public State GetById(int Id)
        {
            return Context.States.FirstOrDefault(item => item.Id == Id);
        }
        public List<State> Get(Func<State, bool> where)
        {
            return Context.States.Where(where).ToList();
        }

        public void Delete(State item)
        {
            Context.Remove(item);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
