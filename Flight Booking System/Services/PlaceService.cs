using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;

namespace Flight_Booking_System.Services
{
    public class PlaceService : IPlaceservice
    {
        private readonly IPlaceRepository placeRepository;

        public PlaceService(IPlaceRepository placeRepository)
        {
            this.placeRepository = placeRepository;
        }

        //**********************************************

        public List<Place> GetAll(string? include = null)
        {
            return placeRepository.GetAll(include);
        }

        public Place GetById(int id)
        {
            return placeRepository.GetById(id);
        }

        public List<Place> Get(Func<Place, bool> where)
        {
            return placeRepository.Get(where);
        }

        public void Insert(Place item)
        {
            placeRepository.Insert(item);
            Save();
        }

        public void Update(Place item)
        {
            placeRepository.Update(item);
            Save();
        }

        public void Delete(Place item)
        {
            placeRepository.Delete(item);
            Save();
        }

        public void Save()
        {
            placeRepository.Save();
        }

        //----------------------------------------


    }
}
