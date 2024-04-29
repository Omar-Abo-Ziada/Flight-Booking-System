using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface IStateRepository : IRepository<State>
    {
        List<State> GetAll(string? include = null);

        State GetById(int id);

        List<State> Get(Func<State, bool> where);

        void Insert(State item);

        void Update(State item);

        void Delete(State item);

        void Save();
    }
}
