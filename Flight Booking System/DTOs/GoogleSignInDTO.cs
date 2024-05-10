using System.ComponentModel.DataAnnotations;

namespace Flight_Booking_System.DTOs
{
    public class GoogleSignInDTO
    {
        [Required]
        public string IdToken { get; set; }
    }
}
