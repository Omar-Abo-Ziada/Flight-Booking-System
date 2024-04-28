using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("AirPort")]
    public class AirPort
    {
        [Key]
        public int Id { get; set; }

        public List<AirLine>? AirLines { get; set; }
    }
}
