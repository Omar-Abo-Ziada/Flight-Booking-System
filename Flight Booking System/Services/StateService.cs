using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;

namespace Flight_Booking_System.Services
{
    public class StateService
    {
        private readonly IStateRepository stateRepository;

        public StateService(IStateRepository stateRepository)
        {
            this.stateRepository = stateRepository;
        }
        //**********************************************

        public List<State> GetAll(string? include = null)
        {
            return stateRepository.GetAll(include);
        }

        public State GetById(int id)
        {
            return stateRepository.GetById(id);
        }

        public List<State> Get(Func<State, bool> where)
        {
            return stateRepository.Get(where);
        }

        public void Insert(State item)
        {
            stateRepository.Insert(item);
            Save();
        }

        public void Update(State item)
        {
            stateRepository.Update(item);
            Save();
        }

        public void Delete(State item)
        {
            stateRepository.Delete(item);
            Save();
        }

        public void Save()
        {
            stateRepository.Save();
        }

        //----------------------------------------
    }
}
