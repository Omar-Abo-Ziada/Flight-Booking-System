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
        public int PassengerId { get; set; }

        public Passenger Passenger { get; set; }

        [ForeignKey("SeatId")]
        public int SeatId { get; set; }

        public Seat Seat { get; set; }
    }
}
