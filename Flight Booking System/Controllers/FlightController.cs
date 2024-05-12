using AutoMapper;
using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;
using Flight_Booking_System.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepository flightRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper mapper;
        private readonly IAirPortRepository airPortRepository;
        private readonly IPlaneRepository planeRepository;
        private readonly IPassengerRepository passengerRepository;
        private readonly ITicketRepository ticketRepository;
        private readonly ISeatRepository seatRepository;

        public FlightController
            (IFlightRepository flightRepository, IWebHostEnvironment webHostEnvironment ,  IMapper mapper,
            IAirPortRepository airPortRepository , IPlaneRepository planeRepository ,
            IPassengerRepository passengerRepository , ITicketRepository ticketRepository,
            ISeatRepository seatRepository)
        {
            this.flightRepository = flightRepository;
            _webHostEnvironment = webHostEnvironment;
            this.mapper = mapper;
            this.airPortRepository = airPortRepository;
            this.planeRepository = planeRepository;
            this.passengerRepository = passengerRepository;
            this.ticketRepository = ticketRepository;
            this.seatRepository = seatRepository;
        }

        //***********************************************

        [HttpGet]
        public ActionResult<GeneralResponse> Get() 
        {
            List<Flight> flights = flightRepository.GetAllWithAllIncludes();  // saeed : replace get include plane with get with all includes

            List<FlightDTO> flightDTOs = new List<FlightDTO>();

            foreach (Flight flight in flights)
            {
                FlightDTO flightDTO = mapper.Map<Flight, FlightDTO>(flight);

                foreach(Passenger passenger in flight.Passengers)
                {
                    flightDTO.passengerDTOs.Add(new PassengerDTO
                    {
                       Id = passenger.Id,
                       Name = passenger.Name,
                       Gender = passenger.Gender,
                       Age = (int)passenger.Age,
                       IsChild = passenger.IsChild,
                       PassportNum = passenger.PassportNum,
                       NationalId = passenger.NationalId
                    });
                }

                foreach(Ticket ticket in flight.Tickets)
                {
                    flightDTO.ticketDTOs.Add(new TicketDTO
                    {
                        Price = ticket.Price,
                        Class = ticket.Class,
                        PassengerId = ticket.PassengerId,
                        FlightId = ticket.FlightId
                    });
                }

                flightDTO.PlaneId = flight?.Plane?.Id;
                // add plane name too
                flightDTO.PlaneName = flight?.Plane?.Name;

                AirPort? SourceAirport = airPortRepository.GetWithIncludes(flight?.SourceAirportId);

                AirPort? DestinationAirport = airPortRepository.GetWithIncludes(flight?.DestinationAirportId);

                flightDTO.SourceAirportNum = SourceAirport?.AirPortNumber;
                flightDTO.SourceAirportName = SourceAirport?.Name;
                flightDTO.SourceAirportCountryName = SourceAirport?.Country?.Name;
                flightDTO.SourceAirportStateName = SourceAirport?.State?.Name;


                flightDTO.DestinationAirportNum = DestinationAirport?.AirPortNumber;
                flightDTO.DestinationAirportName = DestinationAirport?.Name;
                flightDTO.DestinationAirportCountryName = DestinationAirport?.Country?.Name;
                flightDTO.DestinationAirportStateName = DestinationAirport?.State?.Name;

                flightDTOs.Add(flightDTO);
            }

            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = flightDTOs,
                Message = "All flights"
            };
        }

        [HttpGet("{id:int}")] // from route  
        public ActionResult<GeneralResponse> GetbyId(int id)
        {
            Flight? flightFromDB = flightRepository.GetOneWithAllIncludes(id);
            ///todo handle plane name and id in dto or maher will?????

            if (flightFromDB == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,

                    Data = null,

                    Message = "No Flight Found with this ID"
                };
            }
            else
            {
                FlightDTO flightDTO = mapper.Map<Flight, FlightDTO>(flightFromDB);

                AirPort SourceAirport = airPortRepository.GetWithIncludes(flightFromDB.SourceAirportId);

                AirPort DestinationAirport = airPortRepository.GetWithIncludes(flightFromDB.DestinationAirportId);

                flightDTO.SourceAirportNum = SourceAirport?.AirPortNumber;
                flightDTO.SourceAirportName = SourceAirport?.Name;
                flightDTO.SourceAirportCountryName = SourceAirport?.Country?.Name;
                flightDTO.SourceAirportStateName = SourceAirport?.State?.Name;


                flightDTO.DestinationAirportNum = DestinationAirport?.AirPortNumber;
                flightDTO.DestinationAirportName = DestinationAirport?.Name;
                flightDTO.DestinationAirportCountryName = DestinationAirport?.Country?.Name;
                flightDTO.DestinationAirportStateName = DestinationAirport?.State?.Name;
                

                foreach (Passenger passenger in flightFromDB.Passengers)
                {
                    flightDTO.passengerDTOs.Add(new PassengerDTO
                    {
                        Id = passenger.Id,
                        Name = passenger.Name,
                        Gender = passenger.Gender,
                        Age = (int)passenger.Age,
                        IsChild = passenger.IsChild,
                        PassportNum = passenger.PassportNum,
                        NationalId = passenger.NationalId
                    });
                }

                foreach (Ticket ticket in flightFromDB.Tickets)
                {
                    flightDTO.ticketDTOs.Add(new TicketDTO
                    {
                        Price = ticket.Price,
                        Class = ticket.Class,
                        PassengerId = ticket.PassengerId,
                        FlightId = ticket.FlightId
                    });
                }




                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Data = flightDTO,

                    Message = "Found"
                };
            }
        }

        [HttpPost]
     //   [Authorize]
        public ActionResult<GeneralResponse> Add(FlightWithImgDTO flightDTO)
        {
            string uploadpath = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            string imagename = Guid.NewGuid().ToString() + "_" + flightDTO?.Image?.FileName;
            string filepath = Path.Combine(uploadpath, imagename);
            using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
            {
                flightDTO.Image.CopyTo(fileStream);
            }
            flightDTO.imageURL = imagename;

            if (ModelState.IsValid)
            {
                Plane plane = planeRepository.GetById(flightDTO.PlaneId);

                AirPort? sourceAirport = airPortRepository.GetSourceWithFlights(flightDTO.StartId);
                AirPort? destinationAirport = airPortRepository.GetDsetinationWithFlights(flightDTO.DestinationId);

                Flight flight = new Flight()
                {
                    //Id = flightDTO.Id,
                    imageURL = flightDTO.imageURL,

                    SourceAirportId = flightDTO.StartId,
                    SourceAirport = sourceAirport,

                    DestinationAirportId = flightDTO.DestinationId,
                    DestinationAirport = destinationAirport,

                    ArrivalTime = flightDTO.ArrivalTime,
                    DepartureTime = flightDTO.DepartureTime,
                    Plane = plane,
                };


                if (TimeSpan.TryParse(flightDTO.Duration, out TimeSpan ParsedDuration))
                {
                    flight.Duration = ParsedDuration;
                }
                else
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Message = "Invalid Duration Format , it has to be like this : 'HH:MM:SS'"
                    };
                }

                flightRepository.Insert(flight);

                flightRepository.Save();

                if(sourceAirport?.LeavingFlights == null)
                {
                    sourceAirport.LeavingFlights = new List<Flight>();

                    sourceAirport.LeavingFlights.Add(flight);
                }
                else
                {
                    sourceAirport.LeavingFlights.Add(flight);
                }

                if (destinationAirport?.ArrivingFlights == null)
                {
                    destinationAirport.ArrivingFlights = new List<Flight>();

                    destinationAirport.ArrivingFlights.Add(flight);
                }
                else
                {
                    destinationAirport.ArrivingFlights.Add(flight);
                }

                plane.FlightId = flight.Id;
                plane.Flight = flight;

                planeRepository.Save();
                airPortRepository.Save();
                flightRepository.Save();

                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Message = "Flight Added Successfully",
                };

                // omar : we can use one od them if we needed but I think the General Response is better 
                #region possible return responses

                //return NoContent();

                //return Created($"http://localhost:40640/api/flight{flight.Id}", flight);

                //return CreatedAtAction("GetById", new { id = flight.Id }, flight); 
                #endregion
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,

                    Data = ModelState,

                    Message = "the Model State is not valid"
                };
            }
        }

        [HttpPut]
        //[Authorize]
        public ActionResult<GeneralResponse> Edit(int id, FlightDTO editedFlightDTO)
        {
            Flight? flightFromDB = flightRepository.GetWitPassengers_Tickets(id);   /// todo: saeed : change from getbyid to getWithPassengers_tickets

            if (flightFromDB == null )
            { 
                return new GeneralResponse()
                {
                    IsSuccess = false,

                    Data = null,

                    Message = "No Flight Found with this ID or the IDs are not matched "   // +
                    //" make sure that the original flight ID matches the edited flight ID 
                };
            }
            else
            {
                try
                {

                
                // can't use update here because the same instance is already tracked when I got him by Id
                // so I just map with my self and save changes => also cant use automapper because it creates a new instance and doesn't modify the existed one 
                // so SaveChanges won't take effect unless I Mapped manually
                //flightRepository.Update(editedFlightDTO);

                //Maual Mapping
                if (flightFromDB.DepartureTime != editedFlightDTO.DepartureTime || flightFromDB.ArrivalTime != editedFlightDTO.ArrivalTime
                 || flightFromDB.SourceAirportId != editedFlightDTO.SourceAirportId || flightFromDB.DestinationAirportId != editedFlightDTO.DestinationAirportId)
                {
                    flightFromDB.Passengers = null;
                    flightFromDB.Tickets = null;
                    List<Passenger>? oldFlightPassengers = passengerRepository.GetPassengersForFlightWithAllIncludes(flightFromDB.Id);

                    if(oldFlightPassengers != null)
                    {
                        foreach (Passenger passenger in oldFlightPassengers)
                        {
                            passenger.FlightId = null;
                            passenger.Flight = null;
                            passenger.Ticket = null;
                            passengerRepository.Update(passenger);
                            passengerRepository.Delete(passenger);
                        }
                    }

                    List<Ticket>? oldFlightTickets = ticketRepository.GetWithSeatByFlightId(flightFromDB.Id);
                    if(oldFlightTickets != null ) 
                    { 
                       foreach(Ticket ticket in oldFlightTickets)
                        {
                            // del ticket and seat 
                            ticket.FlightId = null;  
                            ticket.Flight = null;      // no obj from flight included
                            ticket.Passenger = null;
                            ticket.PassengerId = null;
                            ticketRepository.Update(ticket);
                            ticketRepository.Delete(ticket);

                            Seat? seat = seatRepository.GetWithTicket(ticket?.Seat?.Id);
                            seat.TicketId = null;
                            seat.Ticket = null;
                            ticket.Seat = null;
                            seatRepository.Update(seat);
                            seatRepository.Delete(seat);
                        }
                    }

                }

                flightFromDB.imageURL = editedFlightDTO.imageURL;
                flightFromDB.DepartureTime = editedFlightDTO.DepartureTime;
                flightFromDB.ArrivalTime = editedFlightDTO.ArrivalTime;
                flightFromDB.IsActive = editedFlightDTO.IsActive;

                // get sourceAirportId before mapping >> use it to update list of flights in airport
                if( editedFlightDTO.SourceAirportId != flightFromDB.SourceAirportId ) 
                {
                    airPortRepository.GetById(flightFromDB.SourceAirportId) 
                                .LeavingFlights?.Remove(flightFromDB);

                    flightFromDB.SourceAirportId = editedFlightDTO.SourceAirportId;
                    // assign obj too

                    airPortRepository.GetById(flightFromDB.SourceAirportId)
                              .LeavingFlights?.Add(flightFromDB);
                }

                if (editedFlightDTO.DestinationAirportId != flightFromDB.DestinationAirportId)
                {
                    airPortRepository.GetById(flightFromDB.DestinationAirportId)
                                .ArrivingFlights?.Remove(flightFromDB);

                    flightFromDB.DestinationAirportId = editedFlightDTO.DestinationAirportId;

                    airPortRepository.GetById(flightFromDB.DestinationAirportId)
                              .ArrivingFlights?.Add(flightFromDB);
                }

                // check if flight plane changed >> change in d.b
                Plane oldPlane = planeRepository.GetByFlightId(flightFromDB.Id);
                Plane newPlane = planeRepository.GetById(editedFlightDTO.PlaneId);

                if (oldPlane?.Id != newPlane.Id)         // front : if flight has no plane yet >> dropdown list >> no assigned plane yet
                {
                    if(oldPlane != null)
                        {
                            oldPlane.FlightId = null;
                            oldPlane.Flight = null;
                            planeRepository.Update(oldPlane);
                        }

                    //    flightFromDB.Plane = newPlane;

                        if (newPlane.FlightId != null)
                    {
                       Flight anotherFlightWithSamePlane = flightRepository.GetById(newPlane.FlightId);
                            // saeed : flight : plane >> should be 1 : m to make this check
                            if (editedFlightDTO.DepartureTime >= anotherFlightWithSamePlane.DepartureTime &&
                                editedFlightDTO.DepartureTime < anotherFlightWithSamePlane.ArrivalTime) 
                            { 
                                return new GeneralResponse
                            {
                                IsSuccess = false,
                                Data = editedFlightDTO,
                                Message = "selected plane is already assigned for another flight in same time"
                            };
                            }
                    }
                        flightFromDB.Plane = newPlane;
                        newPlane.FlightId = flightFromDB.Id;
                        newPlane.Flight = flightFromDB;
                        planeRepository.Update(newPlane);
                }

               
                if (TimeSpan.TryParse(editedFlightDTO.Duration , out TimeSpan ParsedDuration))
                {
                    flightFromDB.Duration = ParsedDuration;
                }
                else
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "Invalid Duration Format , it has to be like this : 'HH:MM:SS'"
                    };
                }

                airPortRepository.Save();
                flightRepository.Save();
                ticketRepository.Save();
                seatRepository.Save();
                planeRepository.Save();


                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Data = editedFlightDTO,

                    Message = "Flight Edited Successfully",
                };
                }
                catch(Exception ex)
                {
                    return new GeneralResponse
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = ex.Message
                    };
                }
                // omar : we can use one od them if we needed but I think the General Response is better 
                #region possible return responses
                // omar : possible return responses :

                //return NoContent();

                //return Created($"http://localhost:40640/api/flight{editedFlight.Id}", editedFlight);

                //return CreatedAtAction("GetById", new { id = editedFlight.Id }, editedFlight); 
                #endregion
            }
        }

        [HttpDelete("{id:int}")] // from route
        //[Authorize]
        public ActionResult<GeneralResponse> Delete(int id)
        {
            Flight? flightFromDB = flightRepository.GetById(id);

            if (flightFromDB == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,

                    Data = null,

                    Message = "No Flight Found with this ID",
                };
            }
            else
            {
                try
                {
                    List<Passenger> flightPassengers = passengerRepository.Get(p => p.FlightId == flightFromDB.Id);

                    foreach (Passenger passenger in flightPassengers)
                    {
                        passenger.FlightId = null;
                        passenger.Flight = null;
                        passenger.Ticket = null;
                        passengerRepository.Update(passenger);
                    }

                    Plane? flightPlane = planeRepository.Get(p => p.FlightId == flightFromDB.Id).FirstOrDefault();

                    if (flightPlane != null)
                    {
                        flightPlane.FlightId = null;
                        flightPlane.Flight = null;
                        planeRepository.Update(flightPlane);
                    }
                    


                    List<Ticket> flightTickets = ticketRepository.Get(t => t.FlightId == flightFromDB.Id);

                   
                    foreach (Ticket ticket in flightTickets)
                    { 
                        Seat seat = seatRepository.Get(s => s.TicketId == ticket.Id).First();
                        ticket.FlightId = null;
                        ticket.Flight = null;
                        ticket.Seat = null;
                        ticket.Passenger = null;
                        seat.TicketId = null; 
                        seat.Ticket = null;
                        seatRepository.Delete(seat);
                        ticketRepository.Delete(ticket); 
                    }

                    seatRepository.Save();
                    ticketRepository.Save();


                    // delete flight from source airport leaving flights list

                    AirPort? sourceAirport = airPortRepository.GetSourceWithFlights(flightFromDB.SourceAirportId);  ///todo : can access after deletion????
                    if(sourceAirport.LeavingFlights != null) 
                    {
                        sourceAirport.LeavingFlights.Remove(flightFromDB);
                    }
                


                    // delete flight from destination airport arriving flights list


                    AirPort? destinationAirport = airPortRepository.GetDsetinationWithFlights(flightFromDB.DestinationAirportId);  ///todo : can access after deletion????
                    if (destinationAirport.ArrivingFlights != null)
                    {
                        destinationAirport.ArrivingFlights.Remove(flightFromDB);
                    }

                    flightRepository.Delete(flightFromDB);

                    flightRepository.Save();

                    return new GeneralResponse()
                    {
                        IsSuccess = true,

                        Data = null,

                        Message = "Flight Deleted Successfully",
                    };

                    // omar : possible return response :
                    //return NoContent();
                }
                catch (Exception ex)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,

                        Data = null,

                        Message = ex.Message,
                    };
                }
            }
        }
    }
}