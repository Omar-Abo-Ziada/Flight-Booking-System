using System.ComponentModel.DataAnnotations;

namespace Flight_Booking_System.DTOs
{
    public class AirPortDTO
    {
        public int Id { get; set; }
        public int? AirPortNumber { get; set; }
        [MaxLength(40)]
        public string? Name { get; set; }
    }
}
