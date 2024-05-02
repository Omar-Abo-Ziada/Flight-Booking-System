using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("States")]
    public class State
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        //------------------------------------

        [ForeignKey("Country")]
        public int? CountryId { get; set; }

        public Country? Country { get; set; }

        [ForeignKey("Place")]
        public int? PlaceId { get; set; }

        public Place? Place { get; set; }
    }
}
