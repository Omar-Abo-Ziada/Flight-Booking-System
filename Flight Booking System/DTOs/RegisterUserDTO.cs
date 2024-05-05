using System.ComponentModel.DataAnnotations;

namespace Flight_Booking_System.DTOs
{
    public class RegisterUserDTO
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
