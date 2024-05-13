using Flight_Booking_System.Enums;
using Flight_Booking_System.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flight_Booking_System.DTOs
{
    public class PassengerDTO
    {
        [Required(ErrorMessage = "The Passenger ID is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Passenger Name ID is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Passenger Gender is required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "The Passenger Age is required")]

        public int Age { get; set; }

        public bool IsChild { get; set; }

  //      [Required(ErrorMessage = "Passport Number is required")]
        public string? PassportNum { get; set; }

    //    [Required(ErrorMessage = "The National ID is required")]
        public string? NationalId { get; set; }

        public int? FlightId { get; set; }

        //----------------------------------------

        //[ForeignKey("Ticket")]
        //public int? TicketId { get; set; }

        //public Ticket? Ticket { get; set; }

    }
}
