using FluentValidation;
using Horeca.Shared.Dtos.Ingredients;

namespace Horeca.Core.Validators
{
    public class MutateIngredientDtoValidator : AbstractValidator<MutateIngredientDto>
    {
        public MutateIngredientDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.IngredientType).NotEmpty().WithMessage("IngredientType is required");
            RuleFor(x => x.BaseAmount).GreaterThan(0).WithMessage("Base amount should be greater than 0");
            RuleFor(x => x.Unit).NotNull().WithMessage("Unit is required");
        }
    }
}