using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;
using Flight_Booking_System.Repositories;
using Flight_Booking_System.Response;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly IAirLineRepository airLineRepository;

        public AirlineController(IAirLineRepository airLineRepository)
        {
            this.airLineRepository = airLineRepository;
        }


        [HttpPost]
        public ActionResult<GeneralResponse> CreateAirline(AirlineDTO airlineDTO)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    AirLine airLine = new AirLine()
                    {
                        Name = airlineDTO.Name,
                        AirlineNumber = airlineDTO.AirlineNumber,
                        AirportId = airlineDTO.AirportId,
                    };
                    airLineRepository.Insert(airLine);
                    airLineRepository.Save();
                    return new GeneralResponse
                    {
                        IsSuccess = true,
                        Data = airlineDTO,
                        Message = "airline added successfully"

                    };
                }
                else
                {
                    return new GeneralResponse
                    {
                        IsSuccess = false,
                        Message = "invalid model satate"
                    };
                }



            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse
                {
                    IsSuccess = false,
                    Message = $"error: {ex.Message}"

                });
            }

        }




        [HttpGet]
        public ActionResult<GeneralResponse> get()
        {

            try
            {
                List<AirLine> airLines = airLineRepository.GetAll();

                var airlineDTOs = airLines.Select(airline => new AirlineDTO()
                {
                    Name = airline.Name,
                    AirlineNumber = airline.AirlineNumber,
                    AirportId = airline.AirportId
                });

                return new GeneralResponse()
                {
                    Data = airlineDTOs,
                    IsSuccess = true,
                    Message = "all airlines for u"

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
        public ActionResult<GeneralResponse> Getbyid(int id)
        {
            try
            {
                AirLine airLine = airLineRepository.GetById(id);
                if (airLine is null)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "invalid id number"
                    };
                }
                else
                {
                    AirlineDTO airlineDTO = new AirlineDTO
                    {
                        Name = airLine.Name,
                        AirlineNumber = airLine.AirlineNumber,
                        AirportId = airLine.AirportId
                    };


                    return new GeneralResponse()
                    {
                        IsSuccess = true,
                        Data = airlineDTO,
                        Message = "that is the target airline"
                    };
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse
                {
                    IsSuccess = false,
                    Message = $"eroor{ex.Message}"

                });
            }
        }



        [HttpDelete("{id}")]
        public ActionResult<GeneralResponse> Delete(int id)
        {
            try
            {

                AirLine airLine = airLineRepository.GetById(id);
                if (airLine is null)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "cant delete invalid id number"
                    };
                }
                else
                {
                    airLineRepository.Delete(airLine);
                    airLineRepository.Save();

                    AirlineDTO airlineDTO = new AirlineDTO
                    {
                        Name = airLine.Name,
                        AirlineNumber = airLine.AirlineNumber,
                        AirportId = airLine.AirportId
                    };


                    return new GeneralResponse()
                    {
                        IsSuccess = true,
                        Data = airlineDTO,
                        Message = "that is the target airline"
                    };
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse
                {
                    IsSuccess = false,
                    Message = $"eroor{ex.Message}"

                });
            }
        }




        [HttpPut("{id}")]
        public ActionResult<GeneralResponse> Update(int id, AirlineDTO airlineDTO)
        {
            try
            {
                AirLine airLine = airLineRepository.GetById(id);
                if (airLine is null)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "invalid id number"
                    };
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        airLine.Name = airlineDTO.Name;
                        airLine.AirlineNumber = airlineDTO.AirlineNumber;
                        airLine.AirportId = airlineDTO.AirportId;

                        airLineRepository.Update(airLine);
                        airLineRepository.Save();

                        return new GeneralResponse()
                        {
                            IsSuccess = true,
                            Data = airlineDTO,
                            Message = "updated sucessfully"
                        };
                    }

                    else
                    {
                        return new GeneralResponse()
                        {
                            IsSuccess = false,

                            Message = "model state is invalid"
                        };
                    }



                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse
                {
                    IsSuccess = false,
                    Message = $"eroor{ex.Message}"

                });
            }
        }

    }
}
