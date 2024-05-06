using System.ComponentModel.DataAnnotations;

namespace Flight_Booking_System.DTOs
{
    public class CountryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Country Name is required")]
        public string? Name { get; set; }

        public int? AirPortId { get; set; }
    }
}
