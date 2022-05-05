using FluentValidation;
using Horeca.Core.Handlers.Commands.Units;

namespace Horeca.Core.Validators.Update
{
    public class UpdateUnitValidator : AbstractValidator<EditUnitCommand>
    {
        public UpdateUnitValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
