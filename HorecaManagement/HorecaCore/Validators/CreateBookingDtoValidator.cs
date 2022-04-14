using FluentValidation;
using Horeca.Core.Handlers.Commands.Bookings;

namespace Horeca.Core.Validators
{
    public class CreateBookingDtoValidator : AbstractValidator<AddBookingCommand>

    {
        public CreateBookingDtoValidator()
        {
            RuleFor(x => x.Model.Pax).NotEmpty().GreaterThan(0).WithMessage("amount of persons has to be larger than 0");
            RuleFor(x => x.Model.ScheduleId).NotEmpty().WithMessage("Schedule Id cannot be empty");
            RuleFor(x => x.Model.Booking).NotEmpty().WithMessage("Booking cannot be empty");
        }
    }
}