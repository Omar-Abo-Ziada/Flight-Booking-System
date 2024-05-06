using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("Countries")]
    public class Country
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        //---------------------------------------

        [ForeignKey("AirPort")]
        public int? AirPortId { get; set; }

        public AirPort? AirPort { get; set; }

        //---------------------------------------

        public List<State>? States { get; set; }
    }
}
