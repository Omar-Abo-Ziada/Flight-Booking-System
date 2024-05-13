using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Flight_Booking_System.Services
{
    public class PhoneConfirmationService : IPhoneConfirmationService
    {
        private readonly TwilioSettings _twilioSettings;

        public PhoneConfirmationService(IOptions<TwilioSettings> twilioSettings)
        {
            _twilioSettings = twilioSettings.Value;
        }

        public MessageResource SendVerificationCode(string phoneNumber, string bodyof_message_verificationCode)
        {
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);

            var result = MessageResource.Create(
                to: phoneNumber,
                from: new Twilio.Types.PhoneNumber(_twilioSettings.PhoneNumber),
                body: bodyof_message_verificationCode
                );

            return result;
        }
    }
}

