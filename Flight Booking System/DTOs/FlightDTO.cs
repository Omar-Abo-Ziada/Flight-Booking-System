using Flight_Booking_System.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flight_Booking_System.DTOs
{
    public class FlightDTO
    {
        //[Key]
        [Required(ErrorMessage = "The Flight ID is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The departure Place is required")]
        public Place Start { get; set; }

        [Required(ErrorMessage = "The Destiantion Place is required")]
        public Place Destiantion { get; set; }

        [Required(ErrorMessage = "The departure time is required")]
        public DateTime DepartureTime { get; set; }

        [Required(ErrorMessage = "The Arrival time is required")]
        public DateTime ArrivalTime { get; set; }

        //-----------------------------------------

        [ForeignKey("Plane")]
        public int? PlaneId { get; set; }

        // public Plane? Plane { get; set; }  // hide in DTO to avoid cyclic refernce exception if any includes are required

        // public List<Passenger>? Passengers { get; set; } // hide in DTO to avoid cyclic refernce exception if any includes are required
    }
}
