using AutoMapper;
using Flight_Booking_System.Context;
using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;
using Flight_Booking_System.Response;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepository flightRepository;
        private readonly IMapper mapper;
        private readonly ITicketRepository ticketRepository;
        private readonly IPlaceRepository placeRepository;

        public ITIContext ITIContext { get; }
        public IPlaneRepository PlaneRepository { get; }

        public FlightController
        (IFlightRepository flightRepository,
            IMapper mapper,
            ITicketRepository ticketRepository,
            ITIContext iTIContext,
            IPlaceRepository placeRepository,
            IPlaneRepository planeRepository)
        {
            this.flightRepository = flightRepository;
            this.mapper = mapper;
            this.ticketRepository = ticketRepository;
            ITIContext = iTIContext;
            this.placeRepository = placeRepository;
            PlaneRepository = planeRepository;
        }

        //***********************************************

        [HttpGet]
        public ActionResult<GeneralResponse> Get()
        {
            List<Flight> flights = flightRepository.GetAll();

            List<FlightDTO> flightDTOs = new List<FlightDTO>();

            foreach (Flight flight in flights)
            {
                //FlightDTO flightDTO = new FlightDTO()
                //{
                //    Id = flight.Id,

                //    //Start = flight.Start,
                //    //Destiantion = flight.Destination,

                //    DepartureTime = flight.DepartureTime,
                //    ArrivalTime = flight.ArrivalTime,

                //    PlaneId = flight.PlaneId,
                //    DestinationId = flight.DestinationId,
                //    StartId = flight.StartId,
                //};

                FlightDTO flightDTO = mapper.Map<Flight, FlightDTO>(flight);

                flightDTO.PlaneName = PlaneRepository.Get(p => p.FlightId == flight.Id).Select(p => p.Name).FirstOrDefault();

                List<Place> FlightPlaces = placeRepository.GetAllWithChilds(flight.Id);

                flightDTO.StartCountryName = FlightPlaces.Where(p => p.DepartingFlights.Contains(flight))
                                             .Select(p => p.Country.Name).FirstOrDefault();

                flightDTO.StartStateName = FlightPlaces.Where(p => p.DepartingFlights.Contains(flight))
                                           .Select(p => p.State.Name).FirstOrDefault();

                flightDTO.DestainationCountryName = FlightPlaces.Where(p => p.DepartingFlights.Contains(flight))
                                           .Select(p => p.Country.Name).FirstOrDefault();

                flightDTO.DestainationStateName = FlightPlaces.Where(p => p.DepartingFlights.Contains(flight))
                                           .Select(p => p.State.Name).FirstOrDefault();


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
            Flight? flightFromDB = flightRepository.GetById(id);

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
                //FlightDTO flightDTO = new FlightDTO()
                //{
                //    Id = flightFromDB.Id,

                //    //Start = flightFromDB.Start,
                //    //Destiantion = flightFromDB.Destination,

                //    DepartureTime = flightFromDB.DepartureTime,
                //    ArrivalTime = flightFromDB.ArrivalTime,

                //    PlaneId = flightFromDB.PlaneId,
                //    DestinationId = flightFromDB.DestinationId,
                //    StartId = flightFromDB.StartId,
                //};

                FlightDTO flightDTO = mapper.Map<Flight, FlightDTO>(flightFromDB);


                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Data = flightDTO,

                    Message = "Found"
                };
            }
        }

        [HttpPost]
        //[Authorize]
        public ActionResult<GeneralResponse> Add(FlightDTO flightDTO)
        {
            if (ModelState.IsValid)
            {
                Flight flight = new Flight();

                flight = mapper.Map<FlightDTO, Flight>(flightDTO);

                flightRepository.Insert(flight);

                flightRepository.Save();

                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Data = flight,

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

        [HttpPut("{id:int}")]
        //[Authorize]
        public ActionResult<GeneralResponse> Update(int id, FlightDTO editedFlightDTO)
        {
            Flight? flightFromDB = flightRepository.GetById(id);

            if (flightFromDB == null || editedFlightDTO.Id != id)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,

                    Data = null,

                    Message = "No Flight Found with this ID or the IDs are not matched , \n" +
                    " make sure that the original flight ID matches the edited flight ID .",
                };
            }
            else
            {
                //flightFromDB = mapper.Map<FlightDTO, Flight>(editedFlightDTO);

                // I Must Map properties manually from editedFlightDTO to flightFromDB
                #region Why can't use Automapper here
                //When you use AutoMapper to map a DTO to an entity, it typically creates a new instance of the entity
                //and copies the values from the DTO to the corresponding properties of the entity.
                //This process doesn't directly update the existing tracked entity in the DbContext.
                //Instead, it creates a new detached entity instance. 
                #endregion

                flightFromDB.StartId = editedFlightDTO.StartId;
                flightFromDB.DestinationId = editedFlightDTO.DestinationId;
                flightFromDB.DepartureTime = editedFlightDTO.DepartureTime;
                flightFromDB.ArrivalTime = editedFlightDTO.ArrivalTime;
                flightFromDB.AirLineId = editedFlightDTO.AirLineId;
                //flightFromDB.PlaneId = editedFlightDTO.PlaneId;
                //flightFromDB.Duration = editedFlightDTO.Duration;  // the next lines trying to parse the string correctly to map this prop

                // Convert string representation of duration to TimeSpan
                if (!string.IsNullOrEmpty(editedFlightDTO.Duration))
                {
                    TimeSpan duration;
                    if (TimeSpan.TryParseExact(editedFlightDTO.Duration, "hh\\:mm\\:ss", null, out duration))
                    {
                        flightFromDB.Duration = duration;
                    }
                    else
                    {
                        // Handle parsing error
                        return new GeneralResponse()
                        {
                            IsSuccess = false,
                            Data = null,
                            Message = "Invalid duration format in the DTO.",
                        };
                    }
                }
                else
                {
                    // Handle null or empty duration string
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "Duration is required.",
                    };
                }

                //ITIContext.Entry(flightFromDB).State = EntityState.Detached;

                //flightRepository.Update(flightFromDB);

                flightRepository.Save();

                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = editedFlightDTO,

                    Message = "Flight Edited Successfully",
                };

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
                    Ticket? ticketfromDB = ticketRepository.Get(t => t.FlightId == id).FirstOrDefault();
                    ticketfromDB.FlightId = null;

                    flightFromDB.AirLineId = null;
                    //flightFromDB.PlaneId = null;
                    flightFromDB.StartId = null;
                    flightFromDB.DestinationId = null;

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