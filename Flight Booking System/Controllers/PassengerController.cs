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
        public PassengerController(IPassengerRepository _passengerRepository , IMapper mapper)
        {
            this.passengerRepository = _passengerRepository;
            this.mapper = mapper;  
        }
        [HttpGet]
        public ActionResult<GeneralResponse> Get()
        {
            List<Passenger> Passengers = passengerRepository.GetAll();

            List<PassengerDTO> passengerDTOs = new List<PassengerDTO>();

            foreach (Passenger passenger in Passengers)
            {
                //PassengerDTO passenerDTO = new PassengerDTO() 
                //{
                //    Id = passenger.Id,
                //    Name = passenger.Name,
                //    Gender = passenger.Gender,
                //}; // the same thing but prefered using mapper
                var passengerDTOMap = mapper.Map<Passenger, PassengerDTO>(passenger); 

                passengerDTOs.Add(passengerDTOMap);
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
                var passengerDTOMap = mapper.Map<Passenger, PassengerDTO>(passengerFromDB);


                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Data = passengerDTOMap,

                    Message = "Found"
                };
            }
        }

        [HttpPost]
        //[Authorize]
        public ActionResult<GeneralResponse> Add(Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                passengerRepository.Insert(passenger);

                passengerRepository.Save();

                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Data = passenger,

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
