namespace Flight_Booking_System.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll(string? include = null);

        T GetById(int? id);

        List<T> Get(Func<T, bool> where);

        void Insert(T item);

        void Update(T item);

        void Delete(T item);

        void Save();
    }
}
