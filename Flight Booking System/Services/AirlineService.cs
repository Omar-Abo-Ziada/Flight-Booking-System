using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;

namespace Flight_Booking_System.Services
{
    public class AirlineService : IAirLineServices
    {
        private readonly IAirLineRepository airLineRepository;

        public AirlineService(IAirLineRepository airLineRepository)
        {
            this.airLineRepository = airLineRepository;
        }



        public List<AirLine> GetAll(string? include = null)
        {
            return airLineRepository.GetAll(include);
        }

        public AirLine GetById(int id)
        {
            return airLineRepository.GetById(id);
        }


        public List<AirLine> Get(Func<AirLine, bool> where)
        {
            return airLineRepository.Get(where);
        }
        public void Delete(AirLine item)
        {
            airLineRepository.Delete(item);
            Save();
        }

        public void Insert(AirLine item)
        {
            airLineRepository.Insert(item);
            Save();
        }

        public void Update(AirLine item)
        {
            airLineRepository.Update(item);
        }
        public void Save()
        {
            airLineRepository.Save();
        }


    }
}
