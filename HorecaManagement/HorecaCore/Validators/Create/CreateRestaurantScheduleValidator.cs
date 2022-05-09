using FluentValidation;
using Horeca.Core.Handlers.Commands.Schedules;

namespace Horeca.Core.Validators.Create
{
    public class CreateRestaurantScheduleDtoValidator : AbstractValidator<AddScheduleCommand>
    {
        public CreateRestaurantScheduleDtoValidator()
        {
            RuleFor(x => x.Model.RestaurantId).NotEmpty().WithMessage("Restaurant id is required");
            RuleFor(x => x.Model.ScheduleDate).NotEmpty().WithMessage("ScheduleDate is required");
            RuleFor(x => x.Model.EndTime).NotEmpty().WithMessage("EndTime is required");
            RuleFor(x => x.Model.StartTime).NotEmpty().WithMessage("StartTime is required");
            RuleFor(x => x.Model.Capacity).NotEmpty().WithMessage("Capacity is required");
            RuleFor(x => x.Model.AvailableSeat).NotEmpty().WithMessage("AvailableSeat is required");
        }
    }
}