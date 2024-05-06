using System.ComponentModel.DataAnnotations;

namespace Flight_Booking_System.DTOs
{
    public class StateDTO
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

       public int? CountryId { get; set; }
       public string? countryName { get; set; }   // instead of calling more than 1 e.p 
        public int? AirPortId { get; set; }

    }
}
