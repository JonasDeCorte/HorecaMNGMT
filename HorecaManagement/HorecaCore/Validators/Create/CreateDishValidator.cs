using FluentValidation;
using Horeca.Core.Handlers.Commands.Dishes;

namespace Horeca.Core.Validators.Create
{
    public class CreateDishValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Model.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Model.DishType).NotEmpty().WithMessage("DishType is required");
            RuleFor(x => x.Model.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Model.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}