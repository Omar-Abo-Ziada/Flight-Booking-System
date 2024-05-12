using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;
using Flight_Booking_System.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly IAirPortRepository airPortRepository;
        private readonly IFlightRepository flightRepository;
        private readonly IPlaneRepository planeRepository;
        private readonly IPassengerRepository passengerRepository;
        private readonly ISeatRepository seatRepository;
        private readonly ITicketRepository ticketRepository;

        public SystemController
         (IAirPortRepository airPortRepository, IFlightRepository flightRepository,
          IPlaneRepository planeRepository, IPassengerRepository passengerRepository ,
          ISeatRepository seatRepository , ITicketRepository ticketRepository)
        {
            this.airPortRepository = airPortRepository;
            this.flightRepository = flightRepository;
            this.planeRepository = planeRepository;
            this.passengerRepository = passengerRepository;
            this.seatRepository = seatRepository;
            this.ticketRepository = ticketRepository;
        }

        //*********************************************

        [HttpGet]
        public ActionResult<GeneralResponse> Initialize()
        {
            List<Flight> flights = flightRepository.GetAllWithAllIncludes();

            if (flights == null || flights.Count == 0)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Message = "There are no flights to initialize"
                };
            }

            foreach (Flight flight in flights)
            {
                Plane? plane = planeRepository.Get(p => p.FlightId == flight.Id).FirstOrDefault();

                plane.Flight = flight;
                //plane.FlightId = flight?.Id;

                flight.Plane = plane;

                //----------------------------------------------------------

                AirPort? sourceAirport = airPortRepository.GetSourceWithFlights(flight.SourceAirportId);

                if (sourceAirport == null)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Message = $"There is no Aiport with this SourceAirportId={flight.SourceAirportId}"
                    };
                }

                if (sourceAirport.LeavingFlights == null)
                {
                    sourceAirport.LeavingFlights = new List<Flight>();
                    sourceAirport.LeavingFlights.Add(flight);
                }
                else
                {
                    sourceAirport.LeavingFlights.Add(flight);
                }

                //----------------------------------------------------------

                AirPort? destinationAitport = airPortRepository.GetDsetinationWithFlights(flight.DestinationAirportId);

                if (destinationAitport == null)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Message = $"There is no Aiport with this destinationAitportId={flight.DestinationAirportId}"
                    };
                }

                if (destinationAitport.ArrivingFlights == null)
                {
                    destinationAitport.ArrivingFlights = new List<Flight>();
                    destinationAitport.ArrivingFlights.Add(flight);
                }
                else
                {
                    destinationAitport.ArrivingFlights.Add(flight);
                }

                //----------------------------------------------------------

                List<Passenger>? passengers = passengerRepository.GetPassengersForFlightWithAllIncludes(flight.Id);

                if (passengers != null)
                {
                    // saeed
                    List<Ticket> passengersTickets = new List<Ticket>();
                  //  List<Seat> passengersSeats = new List<Seat>();

                    foreach (Passenger passenger in passengers)
                    {
                        passenger.Flight = flight;                  /// todo : should be inside if "passenger may be just user" ???????

                        if(passenger.Ticket != null)  // mean he is a real passenger not user
                        {
                            Ticket? ticket = ticketRepository.Get(t => t.PassengerId == passenger.Id).FirstOrDefault();

                            if (ticket != null)
                            {
                                ticket.Passenger = passenger;       /// todo : should be assigned before?

                                ticket.Flight = flight;
                                ticket.FlightId = flight.Id;

                                passenger.Ticket = ticket;              /// todo : should be assigned before?

                                Seat? seat = seatRepository.Get(s => s.TicketId == ticket.Id).FirstOrDefault();

                                if (seat != null)
                                {
                                    seat.TicketId = ticket.Id;    /// todo : how get seat using tickedud then assign to tickedid???????
                                    seat.Ticket = ticket;
                                    ///todo : initialize seats list 
                                  //  passengersSeats.Add(seat);
                                }

                                ticket.Seat = seat;                    /// todo : should be assigned before?
                            }
                            ///todo : initialize tickets list 
                            passengersTickets.Add(ticket);
                        }
                    }
                    /// todo : initialize lists
                    flight.Passengers = passengers;
                    flight.Tickets = passengersTickets;
                }

                //----------------------------------------------------------
            }

            return new GeneralResponse()
            {
                IsSuccess = true,
                Message = "All Data Have been initialized successfully :D"
            };
        }
    }
}