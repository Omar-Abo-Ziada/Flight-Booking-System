using FluentValidation;

namespace Flight_Booking_System.Models
{
    public class SMSModel
    {
        public string To { get; set; }

        public string From { get; set; }

        public string Text { get; set; }

        public class Validator : AbstractValidator<SMSModel>
        {
            public Validator()
            {
                RuleFor(x => x.To).NotEmpty().WithMessage("To phone number required");
                RuleFor(x => x.From).NotEmpty().WithMessage("From phone number required");
            }
        }
    }
}
