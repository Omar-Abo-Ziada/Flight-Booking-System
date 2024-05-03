using Flight_Booking_System.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Flight_Booking_System.Enums;

namespace Flight_Booking_System.DTOs
{
    public class SeatDTO
    {
        [Required(ErrorMessage = "The Seat Id is required")]

        public int Id { get; set; }


        public int? Number { get; set; }

        public Section? Section { get; set; }

        public int? TicketId { get; set; }


    }
}
