using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;

namespace Flight_Booking_System.Services
{
    public class Ticketservice : ITicketService
    {
        private readonly ITicketRepository ticketRepository;

        public Ticketservice(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        //**********************************************

        public List<Ticket> GetAll(string? include = null)
        {
            return ticketRepository.GetAll(include);
        }

        public Ticket GetById(int id)
        {
            return ticketRepository.GetById(id);
        }

        public List<Ticket> Get(Func<Ticket, bool> where)
        {
            return ticketRepository.Get(where);
        }

        public void Insert(Ticket item)
        {
            ticketRepository.Insert(item);
            Save();
        }

        public void Update(Ticket item)
        {
            ticketRepository.Update(item);
            Save();
        }

        public void Delete(Ticket item)
        {
            ticketRepository.Delete(item);
            Save();
        }

        public void Save()
        {
            ticketRepository.Save();
        }

        //----------------------------------------

    }
}
