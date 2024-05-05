using Flight_Booking_System.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flight_Booking_System.DTOs
{
    public class FlightDTO
    {
        // the extra data needed in the front 

        public string? PlaneName { get; set; }

        public string? StartCountryName { get; set; }
        public string? StartStateName { get; set; }

        public string? DestainationCountryName { get; set; }
        public string? DestainationStateName { get; set; }

        public bool IsActive { get; set; } = false;

        //---------------------------------------------

        //[Key]
        [Required(ErrorMessage = "The Flight ID is required")]
        public int Id { get; set; }

        //[Required(ErrorMessage = "The departure Place is required")]
        //public Place? Start { get; set; }

        //[Required]
        [ForeignKey("Start")]
        public int? StartId { get; set; }

        //[Required(ErrorMessage = "The Destiantion Place is required")]
        //public Place? Destiantion { get; set; }

        //[Required]
        [ForeignKey("Destination")]
        public int? DestinationId { get; set; }

        //[Required(ErrorMessage = "The departure time is required")]
        public DateTime? DepartureTime { get; set; }

        //[Required(ErrorMessage = "The Arrival time is required")]
        public DateTime? ArrivalTime { get; set; }

        //[Required]
        public string? Duration { get; set; }

        //[ForeignKey("AirLine")]
        public int? AirLineId { get; set; }

        //public AirLine? AirLine { get; set; }

        [ForeignKey("Plane")]
        public int? PlaneId { get; set; }


        // public Plane? Plane { get; set; }  // hide in DTO to avoid circular refernce if any includes are required

        //public List<Ticket>? Tickets { get; set; }

        // public List<Passenger>? Passengers { get; set; } // hide in DTO to avoid circular refernce if any includes are required
    }
}
