using FluentValidation;
using Horeca.Core.Handlers.Commands.Dishes;

namespace Horeca.Core.Validators.Create
{
    public class CreateAddIngredientDishValidator : AbstractValidator<AddIngredientDishCommand>
    {
        public CreateAddIngredientDishValidator()
        {
            RuleFor(x => x.Model.Ingredient.BaseAmount).NotEmpty().GreaterThan(0).WithMessage("Base amount has to be larger than 0");
            RuleFor(x => x.Model.Ingredient.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Model.Ingredient.Unit.Name).NotEmpty().WithMessage("unit Name cannot be empty");

            RuleFor(x => x.Model.Ingredient.IngredientType).NotEmpty().WithMessage("type cannot be empty");
            RuleFor(x => x.Model.Id).NotEmpty().WithMessage("dish Id cannot be empty");
        }
    }
}