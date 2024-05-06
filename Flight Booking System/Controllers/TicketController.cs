using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;
using Flight_Booking_System.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository ticketRepository;

        public TicketController(ITicketRepository _ticketRepository)
        {
            ticketRepository = _ticketRepository;
        }


        [HttpGet]
        public ActionResult<GeneralResponse> GetAllTickets()
        {
            List<Ticket> tickets = ticketRepository.GetAll();

            List<TicketDTO> ticketsDTOs = new List<TicketDTO>();

            foreach (Ticket ticket in tickets)
            {
                TicketDTO ticketDTO = new TicketDTO()
                {
                    Id = ticket.Id,
                    Class = ticket.Class,
                    Price = ticket.Price,
                    PassengerId = ticket.PassengerId,
                    FlightId = ticket.FlightId,
                    //SeatId = ticket.SeatId,

                };

                ticketsDTOs.Add(ticketDTO);
            }

            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = ticketsDTOs,
                Message = "All Tickets"
            };
        }


        [HttpGet("{ticketId:int}")]
        public ActionResult<GeneralResponse> GetTicketById(int ticketId)
        {
            Ticket ticket = ticketRepository.GetById(ticketId);
            if (ticket == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = " -- Invalid Ticket ID Number -- "
                };
            }
            else
            {
                TicketDTO ticketDTO = new TicketDTO()
                {
                    Id = ticket.Id,
                    Class = ticket.Class,
                    Price = ticket.Price,
                    FlightId = ticket.FlightId,
                    //SeatId = ticket.SeatId,
                    PassengerId = ticket.PassengerId,
                };

                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = ticketDTO,
                    Message = "this the ticket you searched for "
                };
            }
        }




        [HttpPost]
        public ActionResult<GeneralResponse> AddTicket(TicketDTO ticketDTO)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = new Ticket()
                {
                    Id = ticketDTO.Id,
                    Class = ticketDTO.Class,
                    Price = ticketDTO.Price,
                    FlightId = ticketDTO.FlightId,
                    PassengerId = ticketDTO.PassengerId,
                    //SeatId = ticketDTO.SeatId
                };
                ticketRepository.Insert(ticket);
                ticketRepository.Save();
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = ticket,
                    Message = "Ticket Added Successfully"
                };
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = ModelState,
                    Message = "Somthing wrong"
                };
            }
        }

        [HttpPut("{ticketId:int}")]
        public ActionResult<GeneralResponse> UpdateTicket(int ticketId, TicketDTO ticketDTO)
        {
            Ticket t1 = ticketRepository.GetById(ticketId);

            if (t1 == null || t1.Id != ticketId)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Invalid Ticket ID "
                };
            }
            else
            {
                t1.Id = ticketDTO.Id;
                t1.Class = ticketDTO.Class;
                t1.Price = ticketDTO.Price;
                t1.FlightId = ticketDTO.FlightId;
                //t1.SeatId = ticketDTO.SeatId;
                t1.PassengerId = ticketDTO.PassengerId;

                ticketRepository.Update(t1);
                ticketRepository.Save();
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = t1,
                    Message = "Updated Successfully"
                };
            }


        }

        [HttpDelete("{ticketId:int}")]
        public ActionResult<GeneralResponse> DeleteTicket(int ticketId)
        {
            Ticket ticket = ticketRepository.GetById(ticketId);
            if (ticket == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Invalid Ticket ID"
                };
            }
            else
            {
                ticketRepository.Delete(ticket);
                ticketRepository.Save();
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = null,
                    Message = "Ticket Deleted Successfully"
                };
            }
        }


     

     

    }
}
