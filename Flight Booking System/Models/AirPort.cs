using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("AirPorts")]
    public class AirPort
    {
        [Key]
        public int Id { get; set; }

<<<<<<< HEAD
        public int? AirPortNumber { get; set; }
        public string? Name { get; set; }
=======

        public int? AirPortNumber { get; set; }
        public string? Name { get; set; }



>>>>>>> 95aceb6f38e1d5b14c3f2ee090236d1302548a70
        public List<AirLine>? AirLines { get; set; }
    }
}