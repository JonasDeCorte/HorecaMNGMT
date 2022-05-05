using FluentValidation;
using Horeca.Core.Handlers.Commands.Tables;

namespace Horeca.Core.Validators.Create
{
    public class CreateTableValidator : AbstractValidator<AddTableForRestaurantScheduleCommand>

    {
        public CreateTableValidator()
        {
            RuleFor(x => x.Model.Pax).NotEmpty().GreaterThan(0).WithMessage("amount of persons has to be larger than 0");
            RuleFor(x => x.Model.ScaleX).NotEmpty().GreaterThan(0).WithMessage("ScaleX has to be larger than 0");
            RuleFor(x => x.Model.ScaleY).NotEmpty().GreaterThan(0).WithMessage("ScaleY has to be larger than 0");
            RuleFor(x => x.Model.Width).NotEmpty().GreaterThan(0).WithMessage("Width has to be larger than 0");
            RuleFor(x => x.Model.Height).NotEmpty().GreaterThan(0).WithMessage("Height has to be larger than 0");
            RuleFor(x => x.Model.ScheduleId).NotEmpty().WithMessage("Schedule Id cannot be empty");
            RuleFor(x => x.Model.Src).NotEmpty().WithMessage("Src cannot be empty");
        }
    }
}