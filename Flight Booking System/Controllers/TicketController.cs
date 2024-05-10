using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;
using Flight_Booking_System.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository ticketRepository;
        private readonly IPassengerRepository passengerRepository;
        private readonly IFlightRepository flightRepository;
        private readonly IPlaneRepository planeRepository;
        private readonly ISeatRepository seatRepository;

        public TicketController(ITicketRepository _ticketRepository, IPassengerRepository passengerRepository,
            IFlightRepository flightRepository, IPlaneRepository planeRepository , ISeatRepository seatRepository)
        {
            ticketRepository = _ticketRepository;
            this.passengerRepository = passengerRepository;
            this.flightRepository = flightRepository;
            this.planeRepository = planeRepository;
            this.seatRepository = seatRepository;
        }

        //***********************************************

        [HttpGet]
        public ActionResult<GeneralResponse> GetAllTickets()
        {
            List<Ticket> tickets = ticketRepository.GetAll();

            List<TicketDTO> ticketsDTOs = new List<TicketDTO>();

            foreach (Ticket ticket in tickets)
            {
                TicketDTO ticketDTO = new TicketDTO()
                {
                    //Id = ticket.Id,
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
                    //Id = ticket.Id,
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
                ///TODO : don't forget include passengers also
                Flight? flight = flightRepository.GetWithPlane_Passengers((int)ticketDTO.FlightId);

                if (flight?.Plane?.capacity <= flight?.Passengers?.Count)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "There is no place left on the plane ."
                    };
                }

                Ticket ticket = new Ticket()
                {
                    //Id = ticketDTO.Id,
                    Class = ticketDTO.Class,
                    Price = ticketDTO.Price,

                    FlightId = ticketDTO.FlightId, // from front
                    Flight = flightRepository.GetById((int)ticketDTO.FlightId),

                    PassengerId = ticketDTO.PassengerId,// from front
                    Passenger = passengerRepository.GetById((int)ticketDTO.PassengerId),
                };

                ticketRepository.Insert(ticket);

                ticketRepository.Save();

                Passenger passernger = passengerRepository.GetById((int)ticketDTO.PassengerId);

                passernger.Flight = ticket.Flight;
                passernger.FlightId = ticket.FlightId;
                passernger.Ticket = ticket;

                Flight flightFromDB = flightRepository.GetById((int)ticket.FlightId);

                flightFromDB.Passengers.Add(passernger);
                flightFromDB.Tickets.Add(ticket);

                Seat seat = new Seat()
                {
                    Section = ticketDTO.Section,

                    TicketId = ticket.Id,
                    Ticket = ticket,
                };

                seatRepository.Insert(seat);

                seatRepository.Save();

                seat.Number = seat.Id;
                //ggggggggggg
                ticket.Seat = seat;

                ticketRepository.Save();
                passengerRepository.Save();
                planeRepository.Save();
                flightRepository.Save();

                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = null,
                    Message = "Ticket and Seat were Added Successfully"
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
                //t1.Id = ticketDTO.Id;
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
