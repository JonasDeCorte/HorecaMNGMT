using FluentValidation;
using Horeca.Core.Handlers.Commands.Units;

namespace Horeca.Core.Validators
{
    public class CreateUnitDtoValidator : AbstractValidator<CreateUnitCommand>
    {
        public CreateUnitDtoValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}