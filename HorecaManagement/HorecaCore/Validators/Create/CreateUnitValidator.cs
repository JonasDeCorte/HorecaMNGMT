using FluentValidation;
using Horeca.Core.Handlers.Commands.Units;

namespace Horeca.Core.Validators.Create
{
    public class CreateUnitValidator : AbstractValidator<CreateUnitCommand>
    {
        public CreateUnitValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}