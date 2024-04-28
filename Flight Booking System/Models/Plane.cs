using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("Planes")]
    public class Plane
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int capacity { get; set; } // number of passengers can hold

        //-------------------------------
        // not really necessary info

        public float? Length { get; set; }

        public float? Height { get; set; }

        public float? WingSpan { get; set; }

        public string? Engine { get; set; }
    }
}
