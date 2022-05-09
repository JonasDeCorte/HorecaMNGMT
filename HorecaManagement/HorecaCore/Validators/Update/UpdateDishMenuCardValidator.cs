using FluentValidation;
using Horeca.Core.Handlers.Commands.MenuCards;

namespace Horeca.Core.Validators.Update
{
    public class UpdateDishMenuCardValidator : AbstractValidator<EditDishMenuCardCommand>

    {
        public UpdateDishMenuCardValidator()
        {
            RuleFor(x => x.Model.Dish.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Model.Dish.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Model.Dish.DishType).NotEmpty().WithMessage("DishType is required");
            RuleFor(x => x.Model.Dish.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Model.Dish.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
