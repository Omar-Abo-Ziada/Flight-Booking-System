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
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository countryRepository;

        public CountryController(ICountryRepository _countryRepository)
        {
            countryRepository = _countryRepository;
        }

        [HttpGet]
        public ActionResult<GeneralResponse> GetAll()
        {
            List<Country> countries = countryRepository.GetAll();
            List <CountryDTO> countryDTOs = new List<CountryDTO>();

            foreach(Country country in countries) 
            {
                countryDTOs.Add(new CountryDTO()
                {  
                   Name = country.Name,
                   Id = country.Id,
                   AirPortId = country.AirPortId,
                });
            }
            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = countryDTOs,
                Message = "All countries"
            };
        }


        [HttpGet("{id:int}")]   // from route
        public ActionResult<GeneralResponse> GetById(int id) 
        {
           Country country = countryRepository.GetById(id);
            if(country != null)
            {
                CountryDTO countryDTO = new CountryDTO()
                {
                    Id = country.Id,
                    Name = country.Name,
                    AirPortId = country.AirPortId,
                };
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = countryDTO,
                    Message = "Country by id"
                };
            }
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message = "Invalid id"
            };
        }


        // get func

        [HttpPost] 
        public ActionResult<GeneralResponse> Add(CountryDTO countryDTO)  // complex type is sent on request body
        {
            try
            {
                Country country = new Country()
                {
                    Id = countryDTO.Id,
                    Name = countryDTO.Name,
                    AirPortId = countryDTO.AirPortId,
                };
                countryRepository.Insert(country);
                countryRepository.Save();
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = countryDTO,
                    Message = "Country added"
                };
            }
            catch(Exception ex)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message =ex.Message
                };
            }
        }


        [HttpPut]
        public ActionResult<GeneralResponse> Edit(CountryDTO editedCountryDTO) 
        {
          try
            {
                Country editedCountry = new Country()
                {
                    Id = editedCountryDTO.Id,
                    Name = editedCountryDTO.Name,
                    AirPortId = editedCountryDTO.AirPortId,
                };
                countryRepository.Update(editedCountry);
                countryRepository.Save();
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = editedCountryDTO,
                    Message = "Country edited"
                };

            }
            catch(Exception ex) 
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Error on updating"
                };
            }
        }


        [HttpDelete("{id:int}")]  // from route
        public ActionResult<GeneralResponse>Delete(int id)
        {
           Country country = countryRepository.GetById(id);

           if(country != null)
            {
                try
                {
                    countryRepository.Delete(country);
                    countryRepository.Save();
                    return new GeneralResponse()
                    {
                        IsSuccess = true,
                        Data = country,                    // should return dto but no problem as this data will not be displayed on front 
                        Message = "Country deleted"
                    };
                }
                catch(Exception ex)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = null,
                        Message = "Error on deleting"
                    };
                }
            }
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message = "Invalid Id"
            };
        }





           
    }
}
