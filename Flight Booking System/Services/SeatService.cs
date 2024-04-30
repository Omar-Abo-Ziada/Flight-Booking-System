using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;

namespace Flight_Booking_System.Services
{
    public class SeatService : IseatService
    {
        private readonly ISeatRepository seatRepository;

        public SeatService(ISeatRepository seatRepository)
        {
            this.seatRepository = seatRepository;
        }
        //**********************************************

        public List<Seat> GetAll(string? include = null)
        {
            return seatRepository.GetAll(include);
        }

        public Seat GetById(int id)
        {
            return seatRepository.GetById(id);
        }

        public List<Seat> Get(Func<Seat, bool> where)
        {
            return seatRepository.Get(where);
        }

        public void Insert(Seat item)
        {
            seatRepository.Insert(item);
            Save();
        }

        public void Update(Seat item)
        {
            seatRepository.Update(item);
            Save();
        }

        public void Delete(Seat item)
        {
            seatRepository.Delete(item);
            Save();
        }

        public void Save()
        {
            seatRepository.Save();
        }

        //----------------------------------------
    }
}
