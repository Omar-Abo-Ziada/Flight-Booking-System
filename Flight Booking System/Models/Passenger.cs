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
        public string Name { get; set; }

        public Gender Gender { get; set; }

        [Required]
        public int Age { get; set; }

        public bool IsChild { get; set; }

        public string? PassportNum { get; set; }

        public string? NationalId { get; set; }

        //----------------------------------------

        [ForeignKey("Ticket")]
        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }
    }
}
