using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("AirLines")]
    public class AirLine
    {
        [Key]
        public int Id { get; set; }

<<<<<<< HEAD
=======


>>>>>>> 95aceb6f38e1d5b14c3f2ee090236d1302548a70
        public int? AirlineNumber { get; set; }
        public string? Name { get; set; }

        [ForeignKey("AirPort")]
        public int AirportId { get; set; }
        public AirPort? AirPort { get; set; }
        public List<Flight>? Flights { get; set; }
    }
}