using Flight_Booking_System.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flight_Booking_System.DTOs
{
    public class FlightWithImgDTO
    {
        
        public int Id { get; set; }

        public string? imageURL { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "The Image required")]

        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "The StartId required")]
        public int? StartId { get; set; }


        [Required(ErrorMessage = "The DestinationId required")]
        public int? DestinationId { get; set; }


        public DateTime? DepartureTime { get; set; }

        public DateTime? ArrivalTime { get; set; }

        public TimeSpan? Duration { get; set; }

        [Required(ErrorMessage = "The PlaneId required")]

        public int? PlaneId { get; set; }

        [Required(ErrorMessage = "The AirLineId  is required")]

        public int? AirLineId { get; set; }





    }
}
