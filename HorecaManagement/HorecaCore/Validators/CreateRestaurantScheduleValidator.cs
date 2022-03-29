using FluentValidation;
using Horeca.Shared.Dtos.RestaurantSchedules;

namespace HorecaCore.Validators
{
    public class CreateRestaurantScheduleDtoValidator : AbstractValidator<MutateRestaurantScheduleDto>
    {
        public CreateRestaurantScheduleDtoValidator()
        {
            RuleFor(x => x.RestaurantId).NotEmpty().WithMessage("Restaurant id is required");
            RuleFor(x => x.ScheduleDate).NotEmpty().WithMessage("ScheduleDate is required");
            RuleFor(x => x.EndTime).NotEmpty().WithMessage("EndTime is required");
            RuleFor(x => x.StartTime).NotEmpty().WithMessage("StartTime is required");
            RuleFor(x => x.Capacity).NotEmpty().WithMessage("Capacity is required");
            RuleFor(x => x.AvailableSeat).NotEmpty().WithMessage("AvailableSeat is required");
        }
    }
}