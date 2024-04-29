using Flight_Booking_System.Models;

namespace Flight_Booking_System.Repositories
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        List<Ticket> GetAll(string? include = null);

        Ticket GetById(int id);

        List<Ticket> Get(Func<Ticket, bool> where);

        void Insert(Ticket item);

        void Update(Ticket item);

        void Delete(Ticket item);

        void Save();
    }
}
