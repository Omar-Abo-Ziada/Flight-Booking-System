using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Flight_Booking_System.Repositories
{
    
        public class FlightRepository : Repository<Flight>, IFlightRepository
        {
            public FlightRepository(ITIContext _context) : base(_context)
            {
            }

            public void Insert(Flight item)
            {
                Context.Add(item);
            }

            public void Update(Flight item)
            {
                Context.Update(item);
            }

            public List<Flight> GetAll(string? include = null)
            {
                if (include == null)
                {
                    return Context.Flights.ToList();
                }
                return Context.Flights.Include(include).ToList();
            }

            public Flight GetById(int Id)
            {
                return Context.Flights.FirstOrDefault(item => item.Id == Id);
            }
            public List<Flight> Get(Func<Flight, bool> where)
            {
                return Context.Flights.Where(where).ToList();
            }

            public void Delete(Flight item)
            {
                Context.Remove(item);
            }

            public void Save()
            {
                Context.SaveChanges();
            }
        }
    }
