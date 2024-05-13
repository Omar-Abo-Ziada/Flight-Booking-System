using Twilio.Rest.Api.V2010.Account;

public interface IPhoneConfirmationService
{
    MessageResource SendVerificationCode(string phoneNumber, string bodyof_message_verificationCode);
}