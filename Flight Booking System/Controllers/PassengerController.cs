using AutoMapper;
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

    public class PassengerController : ControllerBase
    {
        readonly IPassengerRepository passengerRepository;
        readonly IMapper mapper;
        private readonly IFlightRepository flightRepository;
        private readonly ITicketRepository ticketRepository;
        public PassengerController(IPassengerRepository _passengerRepository, IMapper mapper, IFlightRepository flightRepository, ITicketRepository ticketRepository)
        {
            this.passengerRepository = _passengerRepository;
            this.mapper = mapper;
            this.flightRepository = flightRepository;
            this.ticketRepository = ticketRepository;
        }

        [HttpGet]
        public ActionResult<GeneralResponse> Get()
        {
            List<Passenger> Passengers = passengerRepository.GetAll();

            List<PassengerDTO> passengerDTOs = new List<PassengerDTO>();

            foreach (Passenger passenger in Passengers)
            {
                PassengerDTO passenerDTO = new PassengerDTO()
                {
                    Id = passenger.Id,
                    Name = passenger.Name,
                    Gender = passenger.Gender,
                    Age = (int)passenger.Age,
                    IsChild = passenger.IsChild,
                    FlightId = passenger.FlightId,
                    PassportNum = passenger.PassportNum,
                    NationalId = passenger.NationalId
                };


                passengerDTOs.Add(passenerDTO);
            }

            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = passengerDTOs,
                Message = "All Passengers"
            };
        }

        //***********************************************

        [HttpGet("{id:int}")]
        public ActionResult<GeneralResponse> GetbyId(int id)
        {
            Passenger? passengerFromDB = passengerRepository.GetById(id);

            if (passengerFromDB == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,

                    Data = null,

                    Message = "No Passenger Found with this ID , try a valid ID "
                };
            }
            else
            {
                PassengerDTO passengerDTO = new PassengerDTO() 
                {
                    Id = passengerFromDB.Id,
                    Name = passengerFromDB.Name,
                    Gender = passengerFromDB.Gender,
                    Age = (int)passengerFromDB.Age,
                    IsChild = passengerFromDB.IsChild,
                    FlightId = passengerFromDB.FlightId,
                    PassportNum = passengerFromDB.PassportNum,
                    NationalId = passengerFromDB.NationalId

                };


                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Data = passengerDTO,

                    Message = "This is the Passenger you searched for "
                };
            }
        }

        [HttpPost]
        //[Authorize]
        public ActionResult<GeneralResponse> Add(PassengerDTO passengerDto)
        {
            ///TODO : Don't forget to send the dto in the params not the model
            if (ModelState.IsValid)
            {
                Flight? flight = flightRepository.GetWithPlane_Passengers(passengerDto.FlightId);

                if (flight?.Plane?.capacity <= flight?.Passengers?.Count)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "There is no place left on the plane ."
                    };
                }

                Passenger passenger = new Passenger()
                {
                    Name = passengerDto.Name,
                    Gender = passengerDto.Gender,
                    Age = passengerDto.Age,
                    IsChild = passengerDto.IsChild,
                    FlightId = passengerDto.FlightId,
                    Flight=flight,
                    PassportNum = passengerDto.PassportNum,
                    NationalId = passengerDto.NationalId

                };
                passengerRepository.Insert(passenger);

                passengerRepository.Save();
                Ticket ticket = ticketRepository.Get(t => t.PassengerId == passenger.Id).FirstOrDefault();
                flight.Passengers.Add(passenger);
                passenger.Ticket = ticket; 

                passengerRepository.Save();
                ticketRepository.Save();
                flightRepository.Save();

                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Data = passenger.Id,

                    Message = "New Passenger Added Successfully",
                };
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
        public ActionResult<GeneralResponse> Edit(int id, Passenger editedPassenger)
        {
            Passenger? passengerFromDB = passengerRepository.GetById(id);

            if (passengerFromDB == null || editedPassenger.Id != id)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,

                    Data = null,

                    Message = "Not Passenger Found"
                };
            }
            else
            {
                passengerRepository.Update(editedPassenger);

                passengerRepository.Save();

                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Data = editedPassenger,

                    Message = "Passenger Edited Successfully",
                };
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<GeneralResponse> Delete(int id)
        {
            Passenger? passengerFromDB = passengerRepository.GetById(id);

            if (passengerFromDB == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,

                    Data = null,

                    Message = "Not Found , try a Valid ID",
                };
            }
            else
            {
                try
                {
                    passengerRepository.Delete(passengerFromDB);

                    passengerRepository.Save();

                    return new GeneralResponse()
                    {
                        IsSuccess = true,

                        Data = null,

                        Message = "Passenger Deleted Successfully",
                    };

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
