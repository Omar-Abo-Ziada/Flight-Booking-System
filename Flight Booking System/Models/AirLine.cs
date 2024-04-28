using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("AirLines")]
    public class AirLine
    {
        [Key]
        public int Id { get; set; }

        public List<Flight>? Flights { get; set; }
    }
}
