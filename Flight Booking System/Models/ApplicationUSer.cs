using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    public class ApplicationUSer : IdentityUser
    {
        [ForeignKey("Passenger")]
        public int? PassengerId { get; set; }

        public Passenger? Passenger { get; set; }
    }
}
