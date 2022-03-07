using FluentValidation;
using Horeca.Shared.Dtos.Dishes;

namespace Horeca.Core.Validators
{
    public class CreateDishDtoValidator : AbstractValidator<MutateDishDto>
    {
        public CreateDishDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");

            RuleFor(x => x.DishType).NotEmpty().WithMessage("DishType is required");

            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        }
    }
}