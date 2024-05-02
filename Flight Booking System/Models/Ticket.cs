using Flight_Booking_System.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public Class Class { get; set; } = Class.Economy;  // default value

        //------------------------------------

        [ForeignKey("PassengerId")]
        public int? PassengerId { get; set; }

        public Passenger? Passenger { get; set; }

        [ForeignKey("SeatId")]
        public int? SeatId { get; set; }

<<<<<<< HEAD
        public Seat? Seat { get; set; } 
=======
        public Seat? Seat { get; set; }

        [ForeignKey("Flight")]
        public int? FlightId { get; set; }

        public Flight? Flight { get; set; }
>>>>>>> 9286b31788740556c8c5c7fcae88af10309a72e9
    }
}