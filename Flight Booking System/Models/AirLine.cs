using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("AirLines")]
    public class AirLine
    {
        [Key]
        public int Id { get; set; }



        public int? AirlineNumber { get; set; }
        public string? Name { get; set; }

        [ForeignKey("AirPort")]
        public int AirportId { get; set; }
        public AirPort? AirPort { get; set; }
        public List<Flight>? Flights { get; set; }
    }
}
