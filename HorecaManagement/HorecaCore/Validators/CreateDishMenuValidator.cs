using FluentValidation;
using Horeca.Core.Handlers.Commands.Menus;

namespace Horeca.Core.Validators
{
    public class CreateDishMenuValidator : AbstractValidator<AddDishMenuCommand>
    {
        public CreateDishMenuValidator()
        {
            RuleFor(x => x.Model.Dish.Price).NotEmpty().GreaterThan(0).WithMessage("price has to be larger than 0");
            RuleFor(x => x.Model.Dish.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Model.Dish.Description).NotEmpty().WithMessage("description cannot be empty");
            RuleFor(x => x.Model.Dish.Category).NotEmpty().WithMessage("category cannot be empty");
            RuleFor(x => x.Model.Dish.DishType).NotEmpty().WithMessage("type cannot be empty");
            RuleFor(x => x.Model.Id).NotEmpty().WithMessage("menu Id cannot be empty");
        }
    }
}