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
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceRepository placeRepository;
        private readonly ICountryRepository countryRepository;
        private readonly IStateRepository stateRepository;

        public PlaceController(IPlaceRepository _placeRepository,
            ICountryRepository _countryRepository, IStateRepository _stateRepository)
        {
            placeRepository = _placeRepository;
            countryRepository = _countryRepository;
            stateRepository = _stateRepository;
        }

        [HttpGet]
        public ActionResult<GeneralResponse> GetAll()
        {
            List<Place> places = placeRepository.GetAll();
            List<PlaceDTO> placeDTOs = new List<PlaceDTO>();

            foreach (Place place in places)
            {
                placeDTOs.Add(new PlaceDTO()
                {
                    //countryName = countryRepository.GetById(place.CountryId).Name,
                    //stateName = stateRepository.GetById(place.StateId).Name
                });
            }
            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = placeDTOs,
                Message = "All places"
            };
        }


        [HttpGet("id:{int}")]   // from route
        public ActionResult<GeneralResponse> GetById(int id) 
        {
            Place place = placeRepository.GetById(id);
            if (place != null)
            {
                PlaceDTO placeDTO = new PlaceDTO()
                {
                    //countryName = countryRepository.GetById(place.CountryId).Name,
                    //stateName = stateRepository.GetById(place.StateId).Name
                };
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = placeDTO,
                    Message = "place by id"
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
        public ActionResult<GeneralResponse> Add(PlaceDTO placeDTO)  // complex type is sent on request body
        {
            try
            {
                Place place = new Place()
                {
                    Id = placeDTO.Id,
                    //CountryId = placeDTO.CountryId,
                    //StateId = placeDTO.StateId,
                };
                placeRepository.Insert(place);           // should throw exc when country id and state id is found!!!!!!!!!!!
                placeRepository.Save();
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = placeDTO,
                    Message = "place added"
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = ex.Message            // can customize msg "place already existing" !!!!!!!!
                };
            }
        }


        [HttpPut]
        public ActionResult<GeneralResponse> Edit(PlaceDTO editedPlaceDTO)
        {
            try
            {
                Place editedPlace = new Place()
                {
                    Id = editedPlaceDTO.Id,
                    //CountryId = editedPlaceDTO.CountryId,
                    //StateId = editedPlaceDTO.StateId,
                };
                placeRepository.Update(editedPlace);
                placeRepository.Save();
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = editedPlaceDTO,
                    Message = "place edited"
                };

            }
            catch (Exception ex)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Error on updating"
                };
            }
        }


        [HttpDelete("id:{int}")]  // from route
        public ActionResult<GeneralResponse> Delete(int id)
        {
            Place place = placeRepository.GetById(id);

            if (place != null)
            {
                try
                {
                    placeRepository.Delete(place);
                    placeRepository.Save();
                    return new GeneralResponse()
                    {
                        IsSuccess = true,
                        Data = place,               // should return dto >> so no circular serialization >> here no matter as this data will not be displayed on front
                        Message = "place deleted"
                    };
                }
                catch (Exception ex)
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
