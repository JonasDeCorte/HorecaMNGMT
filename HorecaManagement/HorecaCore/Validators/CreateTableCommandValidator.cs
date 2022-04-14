using FluentValidation;
using Horeca.Core.Handlers.Commands.Tables;

namespace Horeca.Core.Validators
{
    public class CreateTableCommandValidator : AbstractValidator<AddTableForRestaurantScheduleCommand>

    {
        public CreateTableCommandValidator()
        {
            RuleFor(x => x.Model.Pax).NotEmpty().GreaterThan(0).WithMessage("amount of persons has to be larger than 0");
            RuleFor(x => x.Model.ScheduleId).NotEmpty().WithMessage("Schedule Id cannot be empty");
        }
    }
}