using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;

namespace Flight_Booking_System.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }






        public List<Country> GetAll(string? include = null)
        {
            return countryRepository.GetAll(include);
        }

        public Country GetById(int id)
        {
            return countryRepository.GetById(id);
        }

        public List<Country> Get(Func<Country, bool> where)
        {
            return countryRepository.Get(where);
        }




        public void Delete(Country item)
        {
            countryRepository.Delete(item);
            Save();
        }

        public void Insert(Country item)
        {
            countryRepository.Insert(item);
            Save();
        }

        public void Update(Country item)
        {
            countryRepository.Update(item);
            Save();
        }

        public void Save()
        {
            countryRepository.Save();
        }


    }
}
