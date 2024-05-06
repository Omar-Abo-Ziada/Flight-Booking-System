using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("Flights")]
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public string? imageURL { get; set; }


        public DateTime? DepartureTime { get; set; } = DateTime.Now;

        public DateTime? ArrivalTime { get; set; } = DateTime.Now.AddHours(5);

        public TimeSpan? Duration { get; set; } = TimeSpan.FromHours(5);

        public bool IsActive { get; set; } = false;

        //--------------------------------------

        [ForeignKey("SourceAirport")]
        public int? SourceAirportId { get; set; }

        public AirPort? SourceAirport { get; set; }

        [ForeignKey("DestinationAirport")]
        public int? DestinationAirportId { get; set; }

        public AirPort? DestinationAirport { get; set; }

        //-----------------------------------------

        public Plane? Plane { get; set; }

        //-----------------------------------------

        public List<Passenger>? Passengers { get; set; }

        //-----------------------------------------

        public List<Ticket>? Tickets { get; set; }
    }
}
