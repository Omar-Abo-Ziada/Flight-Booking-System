using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;

namespace Flight_Booking_System.Services
{
    public class FlightService : IFlightService
    {
        private readonly FlightRepository flightRepository;

        public FlightService(FlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        //************************************************


        public List<Flight> GetAll(string? include = null)
        {
            return flightRepository.GetAll(include);
        }

        public Flight GetById(int id)
        {
            return flightRepository.GetById(id);
        }

        public List<Flight> Get(Func<Flight, bool> where)
        {
            return flightRepository.Get(where);
        }

        public void Insert(Flight item)
        {
            flightRepository.Insert(item);
        }

        public void Update(Flight item)
        {
            flightRepository.Update(item);
        }

        public void Delete(Flight item)
        {
            flightRepository.Delete(item);
        }

        public void Save()
        {
            flightRepository.Save();
        }

        //----------------------------------------
    }
}
