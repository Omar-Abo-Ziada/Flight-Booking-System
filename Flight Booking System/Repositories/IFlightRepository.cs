using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    
        public interface IFlightRepository : IRepository<Flight>
        {
            List<Flight> GetAll(string? include = null);

            Flight GetById(int id);

            List<Flight> Get(Func<Flight, bool> where);

            void Insert(Flight item);

            void Update(Flight item);

            void Delete(Flight item);

            void Save();
        }
    }
