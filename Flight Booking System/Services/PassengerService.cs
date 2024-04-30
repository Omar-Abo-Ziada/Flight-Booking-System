using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;

namespace Flight_Booking_System.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepository passengerRepository;

        public PassengerService(IPassengerRepository passengerRepository)
        {
            this.passengerRepository = passengerRepository;
        }


        //**********************************************

        public List<Passenger> GetAll(string? include = null)
        {
            return passengerRepository.GetAll(include);
        }

        public Passenger GetById(int id)
        {
            return passengerRepository.GetById(id);
        }

        public List<Passenger> Get(Func<Passenger, bool> where)
        {
            return passengerRepository.Get(where);
        }

        public void Insert(Passenger item)
        {
            passengerRepository.Insert(item);
            Save();
        }

        public void Update(Passenger item)
        {
            passengerRepository.Update(item);
            Save();

        }

        public void Delete(Passenger item)
        {
            passengerRepository.Delete(item);
            Save();

        }

        public void Save()
        {
            passengerRepository.Save();
        }

        //----------------------------------------







    }
}
