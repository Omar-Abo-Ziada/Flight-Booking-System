using Flight_Booking_System.Enums;
using Flight_Booking_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //----------------------------------------------
        // the extra info required for passenger model

        public Gender Gender { get; set; } = Gender.Male;

        public int Age { get; set; }

        //public bool IsChild { get; set; } = false;

        public string? PassportNum { get; set; } = "123";

        public string? NationalId { get; set; } = "456";

        //public int? FlightId { get; set; } = null;

        //public Flight? Flight { get; set; } = null;
    }
}