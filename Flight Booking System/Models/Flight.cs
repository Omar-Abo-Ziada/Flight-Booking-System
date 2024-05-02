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
        [ForeignKey("Start")]
        public int StartId { get; set; }
        public Place? Start { get; set; }

        [Required]
        [ForeignKey("Destination")]
        public int DestinationId { get; set; }
        public Place? Destiantion { get; set; }

        [Required]
        public DateTime? DepartureTime { get; set; }

        [Required]
        public DateTime? ArrivalTime { get; set; }

        [Required]
        public TimeSpan? Duration { get; set; }

        //-----------------------------------------

        [ForeignKey("Plane")]
        public int? PlaneId { get; set; }

        public Plane? Plane { get; set; }

        public List<Passenger>? Passengers { get; set; }
    }
}
