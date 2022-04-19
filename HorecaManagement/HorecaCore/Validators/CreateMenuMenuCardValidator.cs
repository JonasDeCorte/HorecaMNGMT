using FluentValidation;
using Horeca.Core.Handlers.Commands.MenuCards;

namespace Horeca.Core.Validators
{
    public class CreateMenuMenuCardValidator : AbstractValidator<AddMenuMenuCardCommand>

    {
        public CreateMenuMenuCardValidator()
        {
            RuleFor(x => x.Model.MenuCardId).NotEmpty().WithMessage("menucard  Id cannot be empty");
            RuleFor(x => x.Model.Menu.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Model.Menu.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Model.Menu.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Model.Menu.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}