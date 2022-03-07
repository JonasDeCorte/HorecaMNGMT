using FluentValidation;
using Horeca.Shared.Dtos.Units;

namespace Horeca.Core.Validators
{
    public class CreateUnitDtoValidator : AbstractValidator<MutateUnitDto>
    {
        public CreateUnitDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}