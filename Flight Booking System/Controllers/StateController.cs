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
    public class StateController : ControllerBase
    {
        private readonly IStateRepository stateRepository;
        private readonly ICountryRepository countryRepository;

        public StateController(IStateRepository _stateRepository,
            ICountryRepository _countryRepository)
        {
            stateRepository = _stateRepository;
            countryRepository = _countryRepository;
        }

        [HttpGet]
        public ActionResult<GeneralResponse> GetAll()
        {
            List<State> states = stateRepository.GetAll();
            List<StateDTO> stateDTOs = new List<StateDTO>();

            foreach (State state in states)
            {
                stateDTOs.Add(new StateDTO()
                {
                    Name = state.Name,
                    Id = state.Id,
                    countryName = countryRepository.GetById((int)state.CountryId).Name,
                    CountryId = state.CountryId,
                    AirPortId = state.AirPortId, 
                });
            }
            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = stateDTOs,
                Message = "All states"
            };
        }


        [HttpGet("{id:int}")]   // from route
        public ActionResult<GeneralResponse> GetById(int id)
        {
            State state = stateRepository.GetById(id);
            if (state != null)
            {
                StateDTO stateDTO = new StateDTO()
                {
                    Id = state.Id,
                    Name = state.Name,
                    countryName = countryRepository.GetById((int)state.CountryId).Name,
                    CountryId = state.CountryId,
                    AirPortId = state.AirPortId,
                };
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = stateDTO,
                    Message = "State by id"
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
       

        [HttpGet ("{countryId:int}")]  // from route
        public ActionResult<GeneralResponse> GetByCountryId(int countryId)
        {
            List<State> states = stateRepository.Get(s => s.CountryId == countryId).ToList();
            List<StateDTO> stateDTOs = new List<StateDTO>();

            if(states.Count > 0)
            {
                foreach(State state in states)
                {
                    stateDTOs.Add(new StateDTO()
                    {
                        Name = state.Name,
                        Id = state.Id,
                        countryName = countryRepository.GetById((int)state.CountryId).Name,
                        CountryId = state.CountryId,
                        AirPortId = state.AirPortId,
                    });
                }
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = stateDTOs,
                    Message = "All states in specific country"
                };
            }
            return new GeneralResponse()
            {
                IsSuccess = false,
                Data = null,
                Message = "No available states in this country"
            };
        }

        [HttpPost]
        public ActionResult<GeneralResponse> Add(StateDTO stateDTO)  // complex type is sent on request body
        {
            try
            {
                State state = new State()
                {
                    Id = stateDTO.Id,
                    Name = stateDTO.Name,
                    CountryId = stateDTO.CountryId,
                    AirPortId = stateDTO.AirPortId,
                };

                stateRepository.Insert(state);
                stateRepository.Save();
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = stateDTO,
                    Message = "state added"
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = ex.Message
                };
            }
        }


        [HttpPut]
        public ActionResult<GeneralResponse> Edit(StateDTO editedStateDTO) // return dto
        {
            try
            {
                State editedState = new State()
                {
                    Id = editedStateDTO.Id,
                    Name = editedStateDTO.Name,
                    AirPortId = editedStateDTO.AirPortId,
                    CountryId = editedStateDTO.CountryId,
                };

                stateRepository.Update(editedState);
                stateRepository.Save();
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = editedStateDTO,
                    Message = "State edited"
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


        [HttpDelete("{id:int}")]  // from route
        public ActionResult<GeneralResponse> Delete(int id)
        {
            State state = stateRepository.GetById(id);

            if (state != null)
            {
                try
                {
                    stateRepository.Delete(state);
                    stateRepository.Save();
                    return new GeneralResponse()
                    {
                        IsSuccess = true,
                        Data = state,
                        Message = "state deleted"
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
