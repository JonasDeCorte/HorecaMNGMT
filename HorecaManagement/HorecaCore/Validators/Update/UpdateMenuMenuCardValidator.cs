using FluentValidation;
using Horeca.Core.Handlers.Commands.MenuCards;

namespace Horeca.Core.Validators.Update
{
    public class UpdateMenuMenuCardValidator : AbstractValidator<EditMenuMenuCardCommand>
    {
        public UpdateMenuMenuCardValidator()
        {
            RuleFor(x => x.Model.Menu.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Model.Menu.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Model.Menu.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Model.Menu.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}