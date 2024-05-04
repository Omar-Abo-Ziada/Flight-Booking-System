using AutoMapper;
using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;
using Flight_Booking_System.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepository flightRepository;
        private readonly IMapper mapper;

        public FlightController(IFlightRepository flightRepository , IMapper mapper)
        {
            this.flightRepository = flightRepository;
            this.mapper = mapper;
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
        [Authorize]
        public ActionResult<GeneralResponse> Add(Flight flight)
        {
            if (ModelState.IsValid)
            {
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
        [Authorize]
        public ActionResult<GeneralResponse> Edit(int id, Flight editedFlight)
        {
            Flight? flightFromDB = flightRepository.GetById(id);

            if (flightFromDB == null || editedFlight.Id != id)
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
                flightRepository.Update(editedFlight);

                flightRepository.Save();

                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Data = editedFlight,

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
        [Authorize]
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