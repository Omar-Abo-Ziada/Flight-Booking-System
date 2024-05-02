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

        [MaxLength(40)]
        public string? Name { get; set; }

        public List<AirLine>? AirLines { get; set; }
    }
}