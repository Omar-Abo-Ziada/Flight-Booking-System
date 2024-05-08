using Flight_Booking_System.DTOs;
using Flight_Booking_System.Response;

namespace Flight_Booking_System.Services
{
    public interface IGoogleAuthService
    {
        public Task<GeneralResponse> GoogleSignIn(GoogleSignInDTO model);
    }
}