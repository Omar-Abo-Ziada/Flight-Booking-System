using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
  
        public interface IPassengerRepository : IRepository<Passenger>
        {
            List<Passenger> GetAll(string? include = null);

            Passenger GetById(int id);

            List<Passenger> Get(Func<Passenger, bool> where);

            void Insert(Passenger item);

            void Update(Passenger item);

            void Delete(Passenger item);

            void Save();
        }
    }
