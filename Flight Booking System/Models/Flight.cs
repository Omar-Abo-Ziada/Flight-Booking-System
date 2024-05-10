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

        /// todo : remove the new list and check in the controller
        public List<Passenger>? Passengers { get; set; } = new List<Passenger>();

        //-----------------------------------------

        /// todo : remove the new list and check in the controller
        public List<Ticket>? Tickets { get; set; } = new List<Ticket>();
    }
}
///todo : the passengers added in model creating are not added in the lists in the models => sol : api called intiliz loops on all the FKs and creates lists and add them in it .. and this will be for the all models