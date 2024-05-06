using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("Planes")]
    public class Plane
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; } = "Boeing 737";

        public int? capacity { get; set; } = 10; // number of passengers can hold => small number for testing

        public float? Length { get; set; } = 112.60f;

        public float? Height { get; set; } = 38.5f;

        public float? WingSpan { get; set; } = 122.4f;

        public string? Engine { get; set; } = "CFM56-7B";

        //-------------------------------

        [ForeignKey("Flight")]
        public int? FlightId { get; set; }

        public Flight? Flight { get; set; }
    }
}
