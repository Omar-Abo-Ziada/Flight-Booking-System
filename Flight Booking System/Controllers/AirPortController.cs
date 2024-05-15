using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;
using Flight_Booking_System.Response;
using Microsoft.AspNetCore.Mvc;

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirPortController : ControllerBase
    {
        private readonly IAirPortRepository airPortRepository;

        public AirPortController(IAirPortRepository airPortRepository)
        {
            this.airPortRepository = airPortRepository;
        }


        [HttpPost]
        public ActionResult<GeneralResponse> CreateAirPort(AirPortDTO airPortDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var airPort = new AirPort
                    {
                        Name = airPortDTO.Name,
                        AirPortNumber = airPortDTO.AirPortNumber
                    };

                    airPortRepository.Insert(airPort);
                    airPortRepository.Save();



                    return new GeneralResponse
                    {
                        IsSuccess = true,
                        Data = airPortDTO,
                        Message = "Airport created successfully"
                    };
                }
                else
                {
                    return new GeneralResponse
                    {
                        IsSuccess = false,
                        Message = "model state is invalid"
                    };
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                });
            }
        }






        [HttpGet]

        public ActionResult<GeneralResponse> get()
        {


            try
            {
                List<AirPort> airPorts = airPortRepository.GetAll();
                var airPortDTOs = airPorts.Select(airPort => new AirPortDTO
                {
                    Name = airPort.Name,
                    AirPortNumber = airPort.AirPortNumber,
                    Id = airPort.Id
                });
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = airPortDTOs,
                    Message = "that is all airports"

                };
            }
            catch (Exception ex)
            {

                return StatusCode(500, new GeneralResponse
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                });
            }


        }




        [HttpGet("{id}")]
        public ActionResult<GeneralResponse> getbyid(int id)
        {
            try
            {
                AirPort airPort = airPortRepository.GetById(id);
                if (airPort is null)
                {

                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "invalid id"
                    };
                }
                else
                {

                    AirPortDTO airPortDTO = new AirPortDTO()
                    {
                        Name = airPort.Name,
                        AirPortNumber = airPort.AirPortNumber
                    };
                    return new GeneralResponse()
                    {
                        IsSuccess = true,
                        Data = airPortDTO,
                        Message = $"thats the air port u search for it by id {id}"
                    };
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                });
            }
        }






        [HttpDelete("{id}")]
        public ActionResult<GeneralResponse> Delete(int id)
        {
            try
            {
                AirPort airPort = airPortRepository.GetById(id);
                if (airPort is null)
                {

                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "invalid id"
                    };
                }
                else
                {
                    airPortRepository.Delete(airPort);
                    airPortRepository.Save();

                    AirPortDTO airPortDTO = new AirPortDTO()
                    {
                        Name = airPort.Name,
                        AirPortNumber = airPort.AirPortNumber
                    };

                    return new GeneralResponse()
                    {

                        IsSuccess = true,
                        Data = airPortDTO,
                        Message = "reoved sucesfully"
                    };
                }

            }

            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                });
            }



        }









        [HttpPut("{id}")]
        public ActionResult<GeneralResponse> Edit(int id, AirPortDTO airPortDTO)
        {

            AirPort airPort;
            try
            {
                if (ModelState.IsValid)
                {


                    airPort = airPortRepository.GetById(id);
                    if (airPort is null)
                    {

                        return new GeneralResponse()
                        {
                            IsSuccess = false,
                            Data = null,
                            Message = "invalid id"
                        };
                    }
                    else
                    {
                        airPort.Name = airPortDTO.Name;
                        airPort.AirPortNumber = airPortDTO.AirPortNumber;

                        airPortRepository.Update(airPort);
                        airPortRepository.Save();


                        return new GeneralResponse()
                        {
                            Data = airPortDTO,
                            IsSuccess = true,
                            Message = "updated successfully"
                        };
                    }
                }
                else
                {
                    return new GeneralResponse()
                    {

                        IsSuccess = false,
                        Message = "model state is invalid "
                    };
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new GeneralResponse
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                });
            }




        }






    }
}
