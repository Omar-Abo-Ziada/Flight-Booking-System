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

        [Required]
        public int Number { get; set; }

        public Section Section { get; set; } 
    }
}