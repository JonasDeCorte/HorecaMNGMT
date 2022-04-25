using FluentValidation;
using Horeca.Core.Handlers.Commands.Bookings;

namespace Horeca.Core.Validators
{
    public class CreateBookingValidator : AbstractValidator<AddBookingCommand>

    {
        public CreateBookingValidator()
        {
            RuleFor(x => x.Model.Pax).NotEmpty().GreaterThan(0).WithMessage("amount of persons has to be larger than 0");
            RuleFor(x => x.Model.ScheduleId).NotEmpty().WithMessage("Schedule Id cannot be empty");
            RuleFor(x => x.Model.BookingDate).NotEmpty().WithMessage("Booking date cannot be empty");
            RuleFor(x => x.Model.CheckIn).NotEmpty().WithMessage("Booking check in  cannot be empty");
            RuleFor(x => x.Model.CheckOut).NotEmpty().WithMessage("Booking check out  cannot be empty");
            RuleFor(x => x.Model.FullName).NotEmpty().WithMessage("name cannot be empty");
            RuleFor(x => x.Model.PhoneNo).NotEmpty().WithMessage("phone number cannot be empty");
            RuleFor(x => x.Model.UserId).NotEmpty().WithMessage("user id cannot be empty");
        }
    }
}