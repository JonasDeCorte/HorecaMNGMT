using FluentValidation;
using Horeca.Core.Handlers.Commands.Schedules;

namespace Horeca.Core.Validators.Create
{
    public class CreateScheduleValidator : AbstractValidator<AddScheduleCommand>
    {
        public CreateScheduleValidator()
        {
            RuleFor(x => x.Model.Status).NotEmpty().WithMessage("status cannot is required");
            RuleFor(x => x.Model.ScheduleDate).Must(BeAValidDate).WithMessage("Date must be defined ");
            RuleFor(x => x.Model.StartTime).Must(BeAValidDate).WithMessage("start time must be defined ");
            RuleFor(x => x.Model.EndTime).Must(BeAValidDate).WithMessage("end time must be defined ");
            RuleFor(x => x.Model.AvailableSeat).GreaterThan(0).WithMessage("seats must be greater than 0 ");
            RuleFor(x => x.Model.Capacity).GreaterThan(0).GreaterThanOrEqualTo(x => x.Model.AvailableSeat).WithMessage("capacity must be greater than available seats");
            RuleFor(x => x.Model.RestaurantId).NotEmpty().WithMessage("restaurant id cannot be empty ");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }
    }
}