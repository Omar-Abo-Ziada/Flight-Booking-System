using Flight_Booking_System.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("Seats")]
    public class Seat
    {
        [Key]
        public int Id { get; set; }

        public int? Number { get; set; }

        public Section? Section { get; set; } = Enums.Section.Middle;

        //--------------------------------

        [ForeignKey("Ticket")]
        public int? TicketId { get; set; }

        public Ticket? Ticket { get; set; }
    }
}
