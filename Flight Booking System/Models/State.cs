using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("States")]
    public class State
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        //------------------------------------

        [ForeignKey("AirPort")]
        public int? AirPortId { get; set; }

        public AirPort? AirPort { get; set; }

        //------------------------------------

        [ForeignKey("Country")]
        public int? CountryId { get; set; }

        public Country? Country { get; set; }
    }
}
