using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("AirPorts")]
    public class AirPort
    {
        [Key]
        public int Id { get; set; } 

        public int? AirPortNumber { get; set; }

        public string? Name { get; set; } = string.Empty;

        //----------------------------------------

        public Country? Country { get; set; }

        public State? State { get; set; }

        //----------------------------------------

        public List<Flight>? LeavingFlights { get; set; }

        public List<Flight>? ArrivingFlights { get; set; }
    }
}