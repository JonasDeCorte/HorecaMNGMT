using FluentValidation;
using Horeca.Core.Handlers.Commands.Tables;

namespace Horeca.Core.Validators.Create
{
    public class CreateTableFromFloorplanValidator : AbstractValidator<CreateTablesFromFloorplanCommand>
    {
        public CreateTableFromFloorplanValidator()
        {
            RuleForEach(x => x.Model.Tables).ChildRules(tables =>
            {
                tables.RuleFor(x => x.Pax).NotEmpty().GreaterThan(0).WithMessage("amount of persons has to be larger than 0");
                tables.RuleFor(x => x.ScaleX).NotEmpty().GreaterThan(0).WithMessage("ScaleX has to be larger than 0");
                tables.RuleFor(x => x.ScaleY).NotEmpty().GreaterThan(0).WithMessage("ScaleY has to be larger than 0");
                tables.RuleFor(x => x.Width).NotEmpty().GreaterThan(0).WithMessage("Width has to be larger than 0");
                tables.RuleFor(x => x.Height).NotEmpty().GreaterThan(0).WithMessage("Height has to be larger than 0");
                tables.RuleFor(x => x.ScheduleId).NotEmpty().WithMessage("Schedule Id cannot be empty");
                tables.RuleFor(x => x.Src).NotEmpty().WithMessage("Src cannot be empty");
            });
        }
    }
}
