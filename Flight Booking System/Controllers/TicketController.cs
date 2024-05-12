using Azure.Identity;
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
            IFlightRepository flightRepository, IPlaneRepository planeRepository, ISeatRepository seatRepository)
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
                    Id = ticket.Id,
                    Class = ticket.Class,
                    Price = ticket.Price,
                    PassengerId = ticket.PassengerId,
                    FlightId = ticket.FlightId,

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
                Flight? flight = flightRepository.GetWithPlane_Passengers(ticketDTO.FlightId);

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
                    Id = ticketDTO.Id,
                    Class = ticketDTO.Class,
                    Price = ticketDTO.Price,

                    FlightId = ticketDTO.FlightId, // from front
                    Flight = flightRepository.GetById(ticketDTO.FlightId),

                    PassengerId = ticketDTO.PassengerId,// from front
                    Passenger = passengerRepository.GetById(ticketDTO.PassengerId),
                };

                ticketRepository.Insert(ticket);

                ticketRepository.Save();

                Passenger passernger = passengerRepository.GetById(ticketDTO.PassengerId);

                passernger.Flight = ticket.Flight;
                passernger.FlightId = ticket.FlightId;
                passernger.Ticket = ticket;

                Flight flightFromDB = flightRepository.GetById(ticket.FlightId);

                if (flightFromDB != null)
                {
                    if (flightFromDB.Passengers != null)
                    {
                        flightFromDB.Passengers.Add(passernger);
                    }

                    if (flightFromDB.Tickets != null)
                    {
                        flightFromDB.Tickets.Add(ticket);
                    }
                }

                Seat seat = new Seat()
                {
                    Section = ticketDTO.Section,

                    TicketId = ticket.Id,
                    Ticket = ticket,
                };

                seatRepository.Insert(seat);

                seatRepository.Save();

                seat.Number = seat.Id;

                ticket.Seat = seat;

                ticketRepository.Save();
                passengerRepository.Save();
                planeRepository.Save();
                flightRepository.Save();
                seatRepository.Save();  // if there any error then this line my be the problem

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
            //>>>>>>>>>>>>>>>>>>>>>>>>>> Omar :  Actually I won't complete it => it has no meaning to edit a ticket .. u have to delete it and add a new one   <<<<<<<<<<<<<<<<<<<<<<<<<<<<

            throw new Exception("Don't use it ,Edit Ticket actually has no meaning ,, instead delete ticket and add a new one");

            Ticket? ticket = ticketRepository.GetWithAllIncludes(ticketId);

            if (ticket == null || ticket.Id != ticketId)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "No ticket found with this ID," +
                    " or the ID of the given ticket doesn't match the edited ticket Id."
                };
            }

            // updating primtives first
            ticket.Id = ticketDTO.Id;
            ticket.Class = ticketDTO.Class;
            ticket.Price = ticketDTO.Price;


            // now before updating the FKs => edit the related objs with those FKs first
            Passenger passenger = passengerRepository.GetById(ticketDTO.PassengerId);
            passenger.Ticket = ticket;

            Flight? flight = flightRepository.GetWithTickets(ticketDTO.FlightId);

            ticket.FlightId = ticketDTO.FlightId;
            ticket.PassengerId = ticketDTO.PassengerId;

            ticketRepository.Save();

            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = ticket,
                Message = "Updated Successfully"
            };
        }

        [HttpDelete("{ticketId:int}")]
        public ActionResult<GeneralResponse> DeleteTicket(int ticketId)
        {
            Ticket? ticket = ticketRepository.GetWithAllIncludes(ticketId);

            if (ticket == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Invalid Ticket ID"
                };
            }

            #region Deleting Refrences First

            // I think this way getting the seat directly by the seat obj Id in ticket
            // => is mush faster than looping the all seats in DB using func(s => s.ticketId == ticket.Id)
            Seat? seat = seatRepository.GetById(ticket?.Seat?.Id);

            if (seat != null)
            {
                seat.TicketId = null;    //  neccessary
                seat.Ticket = null;      // not neccessary

                try
                {
                    seatRepository.Delete(seat);
                }
                catch (Exception ex)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = ex.Message,
                        Message = "Couldn't delete the seat related to the ticket due to this error."
                    };
                }
            }

            //-------------------------------------------------------------------


            // I think this way getting the passenger directly by the passenger obj Id in ticket
            // => is mush faster than looping the all passengers and include ticket in DB using func(p => p.ticket.Id == ticket.Id)
            Passenger? passenger = passengerRepository.GetById(ticket?.Passenger?.Id);

            if (passenger != null)
            {
                passenger.Ticket = null;

                passenger.Flight = null;
                passenger.FlightId = null;
            }

            //-------------------------------------------------------------------

            // much faster than => Get(f => f.Tickets.Contains(ticket)).FirstOrDefault();
            Flight? flight = flightRepository.GetWithTickets_Passengers(passenger?.FlightId);

            if (flight != null)
            {
                if (flight.Tickets != null && flight.Tickets.Contains(ticket))
                {
                    flight.Tickets.Remove(ticket);
                }

                if (flight.Passengers != null && flight.Passengers.Contains(passenger))
                {
                    // this passenger has no ticket now => so he is just a user until he book another ticket
                    flight.Passengers.Remove(passenger);
                }
            }

            //-------------------------------------------------------------------

            #endregion

            try
            {
                ticketRepository.Delete(ticket);
            }
            catch (Exception ex)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = ex.Message,
                    Message = "Couldn't delete the ticket due to this error."
                };
            }

            flightRepository.Save();

            passengerRepository.Save();

            ticketRepository.Save();

            seatRepository.Save();

            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = null,
                Message = "Ticket Deleted Successfully"
            };
        }
    }
}
