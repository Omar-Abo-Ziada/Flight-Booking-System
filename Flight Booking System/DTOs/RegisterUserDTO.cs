using System.ComponentModel.DataAnnotations;

namespace Flight_Booking_System.DTOs
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage ="UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("Password" , ErrorMessage ="Passwords don't match")]
        public string ConfirmPassword { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [RegularExpression(@"^01\d{9}$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
