using FluentValidation;
using Horeca.Shared.Dtos;

namespace Horeca.Core.Validators
{
    public class CreateIngredientDtoValidator : AbstractValidator<CreateIngredientDto>
    {
        public CreateIngredientDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}