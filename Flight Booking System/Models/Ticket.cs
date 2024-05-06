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
        public decimal? Price { get; set; } = 1000m;

        [Required]
        public Class Class { get; set; } = Class.Economy;

        //------------------------------------

        [ForeignKey("Flight")]
        public int? FlightId { get; set; }

        public Flight? Flight { get; set; }

        //------------------------------------

        //[ForeignKey("Seat")]
        //public int? SeatId { get; set; }   // => had to put the FK also because u need it in the ticket details for sure

        public Seat? Seat { get; set; }

        //------------------------------------

        [ForeignKey("PassengerId")]
        public int? PassengerId { get; set; }

        public Passenger? Passenger { get; set; }

    }
}