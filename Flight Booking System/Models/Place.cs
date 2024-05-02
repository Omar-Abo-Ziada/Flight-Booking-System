using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("Places")]
    public class Place
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public Country? Country { get; set; }

        [Required]
        [ForeignKey("State")]
        public int StateId { get; set; }

        public State? State { get; set; }

        // Navigation property representing flights departing from this place
        public List<Flight>? DepartingFlights { get; set; }

        // Navigation property representing flights arriving at this place
        public List<Flight>? ArrivingFlights { get; set; }
    }
}
