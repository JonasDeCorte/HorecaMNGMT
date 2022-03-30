using FluentValidation;
using Horeca.Shared.Dtos.Bookings;

namespace Horeca.Core.Validators
{
    public class CreateBookingDtoValidator : AbstractValidator<MakeBookingDto>

    {
        public CreateBookingDtoValidator()
        {
            RuleFor(x => x.Pax).NotEmpty().GreaterThan(0).WithMessage("amount of persons has to be larger than 0");
            RuleFor(x => x.ScheduleID).NotEmpty().WithMessage("Schedule Id cannot be empty");
            RuleFor(x => x.Booking).NotEmpty().WithMessage("Booking cannot be empty");
        }
    }
}