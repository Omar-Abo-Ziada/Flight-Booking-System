using Flight_Booking_System.Enums;
using Flight_Booking_System.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flight_Booking_System.DTOs
{
    public class TicketDTO
    {
        public Section? Section { get; set; } = Enums.Section.Middle;   /// todo : not in model??

        //--------------------------------------------

        [Required(ErrorMessage = "The Ticket Id is required")]
        public int Id { get; set; }

        [Column(TypeName = "money")]
        [Required(ErrorMessage = "The Price  is required")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "The Class  is required")]
        public Class Class { get; set; } // have to be sent from the front-end

        [Required(ErrorMessage = "The Passenger Id is required")]
        public int? PassengerId { get; set; } // have to be sent from the front-end

        [Required(ErrorMessage = "The FlightId  is required")]
        public int? FlightId { get; set; } // have to be sent from the front-end
    }
}