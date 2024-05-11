using Flight_Booking_System.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flight_Booking_System.DTOs
{
    public class FlightDTO
    {
        public int? PlaneId { get; set; }
        // saeed : adding additional info for front
        public int Id { get; set; }
        public string? PlaneName { get; set; } 
        public List<PassengerDTO>? passengerDTOs { get; set; } = new List<PassengerDTO>();
        public List<TicketDTO>? ticketDTOs { get; set; } = new List<TicketDTO>();
        //----------------------------------------

        public int? SourceAirportNum { get; set; }

        public string? SourceAirportName { get; set; }

        public string? SourceAirportCountryName{ get; set; }

        public string? SourceAirportStateName{ get; set; }

        //*******************************************

        public int? DestinationAirportNum { get; set; }

        public string? DestinationAirportName { get; set; }

        public string? DestinationAirportCountryName { get; set; }

        public string? DestinationAirportStateName { get; set; }

        //********************************************

        //public int Id { get; set; }  // testing Ibrahim opinoin

        public string? imageURL { get; set; }

        public DateTime? DepartureTime { get; set; } = DateTime.Now;

        public DateTime? ArrivalTime { get; set; } = DateTime.Now.AddHours(5);

        public string? Duration { get; set; } = "05:30:10";

        public bool IsActive { get; set; } = false;

        //--------------------------------------

        public int? SourceAirportId { get; set; }

        //public AirPort? SourceAirport { get; set; }

        public int? DestinationAirportId { get; set; }

        //public AirPort? DestinationAirport { get; set; }

        //-----------------------------------------

        //public Plane? Plane { get; set; }

        //-----------------------------------------

        //public List<Passenger>? Passengers { get; set; }

        //-----------------------------------------

        //public List<Ticket>? Tickets { get; set; }
    }
}

