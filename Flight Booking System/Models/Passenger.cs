using Flight_Booking_System.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("Passengers")]
    public class Passenger
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; } = string.Empty;

        public Gender Gender { get; set; } = Gender.Male;

        public int? Age { get; set; } = 30;

        public bool IsChild { get; set; } = false;

        public string? PassportNum { get; set; } = "123";

        public string? NationalId { get; set; } = "456";

        //----------------------------------------

        [ForeignKey("Flight")]
        public int? FlightId { get; set; }

        public Flight? Flight { get; set; }

        //----------------------------------------

        //[ForeignKey("Ticket")]
        //public int? TicketId { get; set; } // => had to put the FK here because it's needed in the passenger details
        // couldn't do that because it leads to migrations Error

        public Ticket? Ticket { get; set; }

        //-----------------------------------------
        //[ForeignKey("User")]
        //public int? UserId { get; set; }

        public ApplicationUSer? User { get; set; }

    }
}
