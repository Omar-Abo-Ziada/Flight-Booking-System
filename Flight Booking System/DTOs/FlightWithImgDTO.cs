using Flight_Booking_System.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flight_Booking_System.DTOs
{
    public class FlightWithImgDTO
    {
        public int? PlaneId { get; set; }

        //------------------------------------

        public int Id { get; set; }

        public string? imageURL { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "The Image required")]
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "The Source Airport Id is required")]
        public int? StartId { get; set; }


        [Required(ErrorMessage = "The Destination Airport Id required")]
        public int? DestinationId { get; set; }


        public DateTime? DepartureTime { get; set; }

        public DateTime? ArrivalTime { get; set; }

        public string? Duration { get; set; } 

    }
}
