using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("Places")]
    public class Place
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Country? Country { get; set; }

        [Required]
        public State? State { get; set; }
    }
}
