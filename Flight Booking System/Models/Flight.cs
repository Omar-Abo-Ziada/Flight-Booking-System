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

        [ForeignKey("Start")]
        public int? StartId { get; set; }

        //[ForeignKey("StartId")]
        public Place? Start { get; set; }

        [ForeignKey("Destination")]
        public int? DestinationId { get; set; }

        public Place? Destination { get; set; }

        public DateTime? DepartureTime { get; set; } = DateTime.Now;

        public DateTime? ArrivalTime { get; set; } = DateTime.Now.AddHours(5);

        public TimeSpan? Duration { get; set; } = TimeSpan.FromHours(5);

        public bool IsActive { get; set; } = false;

        //-----------------------------------------

        //[ForeignKey("Plane")]
        //public int? PlaneId { get; set; }

        public Plane? Plane { get; set; }

        [ForeignKey("AirLine")]
        public int? AirLineId { get; set; }

        public AirLine? AirLine { get; set; }

        public List<Passenger>? Passengers { get; set; }

        public List<Ticket>? Tickets { get; set; }
    }
}
