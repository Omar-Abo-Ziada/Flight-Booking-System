using Flight_Booking_System.Context;
using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;
using Flight_Booking_System.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly ITIContext _context;

        public FlightController(ITIContext ITIContext)
        {
            _context = ITIContext;
        }

        //***********************************************

        [HttpGet]
        public ActionResult<GeneralResponse> Get()
        {
            List<Flight> flights = _context.Flights.ToList();

            List<FlightDTO> flightDTOs = new List<FlightDTO>();

            foreach (Flight flight in flights)
            {
                FlightDTO flightDTO = new FlightDTO()
                {
                    Id = flight.Id,

                    Start = flight.Start,
                    Destiantion = flight.Destiantion,

                    DepartureTime = flight.DepartureTime,
                    ArrivalTime = flight.ArrivalTime,

                    PlaneId = flight.PlaneId,
                };

                flightDTOs.Add(flightDTO);
            }

            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = flightDTOs
            };
        }


        //[HttpGet]
        //public ActionResult<GeneralResponse> GetById()
        //{
        //    List<Flight> flights = _context.Flights.ToList();

        //    List<FlightDTO> flightDTOs = new List<FlightDTO>();

        //    foreach (Flight flight in flights)
        //    {
        //        FlightDTO flightDTO = new FlightDTO()
        //        {
        //            Id = flight.Id,

        //            Start = flight.Start,
        //            Destiantion = flight.Destiantion,

        //            DepartureTime = flight.DepartureTime,
        //            ArrivalTime = flight.ArrivalTime,

        //            PlaneId = flight.PlaneId,
        //        };

        //        flightDTOs.Add(flightDTO);
        //    }

        //    return new GeneralResponse()
        //    {
        //        IsSuccess = true,
        //        Data = flightDTOs
        //    };
        //}

        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&


        //[HttpGet("{id:int}")]
        ////[HttpGet] getting the id from query string only
        ////[Authorize]
        //public ActionResult<CategoryDTO> GetById(int id)
        //{
        //    Category categoryFromDB = categoryRepository.GetById(id);

        //    if (categoryFromDB == null)
        //    {
        //        return BadRequest("No Category found with this ID");
        //    }

        //    #region manual mapping
        //    CategoryDTO categoryDTO = new CategoryDTO()
        //    {
        //        Id = categoryFromDB.Id,
        //        Name = categoryFromDB.Name,

        //        ProductsNames = new List<string>(),
        //    };

        //    foreach (Product prod in categoryFromDB.Products)
        //    {
        //        categoryDTO.ProductsNames.Add(new string(prod.Name));
        //    }
        //    #endregion

        //    return Ok(categoryDTO);
        //}

        //[HttpPost]
        //public IActionResult Add(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        categoryRepository.Add(category);
        //        categoryRepository.Save();

        //        return CreatedAtAction("GetById", new { id = category.Id }, category);

        //    }
        //    return BadRequest(ModelState);
        //}

        //[HttpPost]
        //public IActionResult Add(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        productRepository.Add(product);

        //        productRepository.Save();

        //        // return Ok("Product Added Successfully");

        //        //  return Created($"http://localhost:49076/api/Product/{product.Id}" , product);

        //        return CreatedAtAction("GetById", new { id = product.Id }, product);
        //    }
        //    return BadRequest(ModelState);
        //}

        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&


    }
}
