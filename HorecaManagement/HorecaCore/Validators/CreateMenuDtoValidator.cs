using FluentValidation;
using Horeca.Core.Handlers.Commands.Menus;

namespace Horeca.Core.Validators
{
    public class CreateMenuDtoValidator : AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuDtoValidator()
        {
            RuleFor(x => x.Model.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Model.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Model.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Model.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}