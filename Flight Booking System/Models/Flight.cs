using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("Flights")]
    public class Flight
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Place? Start { get; set; }

        [Required]
        public Place? Destiantion { get; set; }

        [Required]
        public DateTime? DepartureTime { get; set; }

        [Required]
        public DateTime? ArrivalTime { get; set; }

        [Required]
        public DateTime? Duration { get; set; }

        //-----------------------------------------

        [ForeignKey("Plane")]
        public int? PlaneId { get; set; }

        public Plane? Plane { get; set; }

        public List<Passenger>? Passengers { get; set; }
    }
}
