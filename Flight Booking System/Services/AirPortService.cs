using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;

namespace Flight_Booking_System.Services
{
    public class AirPortService : IAirPortService
    {
        private readonly IAirPortRepository airPortRepository;

        public AirPortService(IAirPortRepository airPortRepository)
        {
            this.airPortRepository = airPortRepository;
        }

        //**********************************************

        public List<AirPort> GetAll(string? include = null)
        {
            return airPortRepository.GetAll(include);
        }

        public AirPort GetById(int id)
        {
            return airPortRepository.GetById(id);
        }

        public List<AirPort> Get(Func<AirPort, bool> where)
        {
            return airPortRepository.Get(where);
        }

        public void Insert(AirPort item)
        {
            airPortRepository.Insert(item);
        }

        public void Update(AirPort item)
        {
            airPortRepository.Update(item);
        }

        public void Delete(AirPort item)
        {
            airPortRepository.Delete(item);
        }

        public void Save()
        {
            airPortRepository.Save();
        }

        //----------------------------------------

    }
}
