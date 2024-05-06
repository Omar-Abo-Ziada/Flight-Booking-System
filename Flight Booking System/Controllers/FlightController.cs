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

        public FlightController
            (IFlightRepository flightRepository, IWebHostEnvironment webHostEnvironment ,  IMapper mapper,
            IAirPortRepository airPortRepository)
        {
            this.flightRepository = flightRepository;
            _webHostEnvironment = webHostEnvironment;
            this.mapper = mapper;
            this.airPortRepository = airPortRepository;
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

                AirPort SourceAirport = airPortRepository.GetWithIncludes((int)flight.SourceAirportId);

                AirPort DestinationAirport = airPortRepository.GetWithIncludes((int)flight.DestinationAirportId);

                flightDTO.SourceAirportNum = SourceAirport.AirPortNumber;
                flightDTO.SourceAirportName = SourceAirport.Name;
                flightDTO.SourceAirportCountryName = SourceAirport.Country.Name;
                flightDTO.SourceAirportStateName = SourceAirport.State.Name;


                flightDTO.DestinationAirportNum = DestinationAirport.AirPortNumber;
                flightDTO.DestinationAirportName = DestinationAirport.Name;
                flightDTO.DestinationAirportCountryName = DestinationAirport.Country.Name;
                flightDTO.DestinationAirportStateName = DestinationAirport.State.Name;

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

                AirPort SourceAirport = airPortRepository.GetWithIncludes((int)flightFromDB.SourceAirportId);

                AirPort DestinationAirport = airPortRepository.GetWithIncludes((int)flightFromDB.DestinationAirportId);

                flightDTO.SourceAirportNum = SourceAirport.AirPortNumber;
                flightDTO.SourceAirportName = SourceAirport.Name;
                flightDTO.SourceAirportCountryName = SourceAirport.Country.Name;
                flightDTO.SourceAirportStateName = SourceAirport.State.Name;


                flightDTO.DestinationAirportNum = DestinationAirport.AirPortNumber;
                flightDTO.DestinationAirportName = DestinationAirport.Name;
                flightDTO.DestinationAirportCountryName = DestinationAirport.Country.Name;
                flightDTO.DestinationAirportStateName = DestinationAirport.State.Name;


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
            string imagename = Guid.NewGuid().ToString() + "_" + flightDTO.Image.FileName;
            string filepath = Path.Combine(uploadpath, imagename);
            using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
            {
                flightDTO.Image.CopyTo(fileStream);
            }
            flightDTO.imageURL = imagename;

            if (ModelState.IsValid)
            {
                Flight flight = new Flight()
                {
                    Id = flightDTO.Id,
                    //PlaneId= flightDTO.PlaneId,
                    DestinationAirportId = flightDTO.DestinationId,
                    imageURL=flightDTO.imageURL,
                    SourceAirportId = flightDTO.StartId,
                    ArrivalTime= flightDTO.ArrivalTime,
                    DepartureTime= flightDTO.DepartureTime,
                  Duration= flightDTO.Duration,
                  //AirLineId= flightDTO.AirLineId,
                };
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

        [HttpPut]
        //[Authorize]
        public ActionResult<GeneralResponse> Edit(int id, FlightDTO editedFlightDTO)
        {
            Flight? flightFromDB = flightRepository.GetById(id);

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
                // can't use update here because the same instance is already tracked when I got him by Id
                // so I just map with my self and save changes => also cant use automapper because it creates a new instance and doesn't modify the existed one 
                // so SaveChanges won't take effect unless I Mapped manually
                //flightRepository.Update(editedFlightDTO);

                //Maual Mapping
                flightFromDB.imageURL = editedFlightDTO.imageURL;
                flightFromDB.DepartureTime = editedFlightDTO.DepartureTime;
                flightFromDB.ArrivalTime = editedFlightDTO.ArrivalTime;
                flightFromDB.IsActive = editedFlightDTO.IsActive;

                flightFromDB.SourceAirportId = editedFlightDTO.SourceAirportId;
                flightFromDB.DestinationAirportId = editedFlightDTO.DestinationAirportId;

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