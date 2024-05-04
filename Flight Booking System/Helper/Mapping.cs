using AutoMapper;
using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;

namespace Flight_Booking_System.Helper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Passenger, PassengerDTO>(); 

            CreateMap<Flight, FlightDTO>(); 
        }
    }
}
