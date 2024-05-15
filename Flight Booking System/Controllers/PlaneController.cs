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
    public class PlaneController : ControllerBase
    {
        private readonly IPlaneRepository planeRepository;

        public PlaneController(IPlaneRepository planeRepository)
        {
            this.planeRepository = planeRepository;
        }

        [HttpGet("freePlanes")]
        public ActionResult<GeneralResponse> getFreePlanes()
        {
            try
            {
                List<Plane> planes = planeRepository.Get(p => p.FlightId == null).ToList();
                var planeDTOs = planes.Select( p => new PlaneDTO 
                {
                    Name = p.Name,
                    Id = p.Id
                });
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = planeDTOs,
                    Message = "that is all planes"

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


        [HttpPost]
        //[Authorize]
        public ActionResult<GeneralResponse> Add(PlaneDTO planedto)
        {
            ///TODO : Don't forget to send the dto in the params not the model
            ///

            Plane plane = new Plane()
            {
                Name = planedto.Name,
                Id = planedto.Id
            };

            if (ModelState.IsValid)
            {
                planeRepository.Insert(plane);

                planeRepository.Save();

                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Data = plane,

                    Message = "New plane Added Successfully",
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

    }
}
