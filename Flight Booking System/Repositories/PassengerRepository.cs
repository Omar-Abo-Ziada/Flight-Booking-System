using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Flight_Booking_System.Repositories
{
   
        public class PassengerRepository : Repository<Passenger>, IPassengerRepository
        {
            public PassengerRepository(ITIContext _context) : base(_context)
            {
            }

            public void Insert(Passenger item)
            {
                Context.Add(item);
            }

            public void Update(Passenger item)
            {
                Context.Update(item);
            }

            public List<Passenger> GetAll(string? include = null)
            {
                if (include == null)
                {
                    return Context.Passengers.ToList();
                }
                return Context.Passengers.Include(include).ToList();
            }

            public Passenger GetById(int Id)
            {
                return Context.Passengers.FirstOrDefault(item => item.Id == Id);
            }
            public List<Passenger> Get(Func<Passenger, bool> where)
            {
                return Context.Passengers.Where(where).ToList();
            }

            public void Delete(Passenger item)
            {
                Context.Remove(item);
            }

            public void Save()
            {
                Context.SaveChanges();
            }
        }
    }
