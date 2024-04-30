using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;

namespace Flight_Booking_System.Services
{
    public class PlaneService : IPlaneService
    {
        private readonly IPlaneRepository planeRepository;

        public PlaneService(IPlaneRepository planeRepository)
        {
            this.planeRepository = planeRepository;
        }


        //**********************************************

        public List<Plane> GetAll(string? include = null)
        {
            return planeRepository.GetAll(include);
        }

        public Plane GetById(int id)
        {
            return planeRepository.GetById(id);
        }

        public List<Plane> Get(Func<Plane, bool> where)
        {
            return planeRepository.Get(where);
        }

        public void Insert(Plane item)
        {
            planeRepository.Insert(item);
            Save();
        }

        public void Update(Plane item)
        {
            planeRepository.Update(item);
            Save();
        }

        public void Delete(Plane item)
        {
            planeRepository.Delete(item);
            Save();
        }

        public void Save()
        {
            planeRepository.Save();
        }

        //----------------------------------------
    }
}
