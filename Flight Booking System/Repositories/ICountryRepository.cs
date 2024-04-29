using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    
        public interface ICountryRepository : IRepository<Country>
        {
            List<Country> GetAll(string? include = null);

            Country GetById(int id);

            List<Country> Get(Func<Country, bool> where);

            void Insert(Country item);

            void Update(Country item);

            void Delete(Country item);

            void Save();
        }
    }
