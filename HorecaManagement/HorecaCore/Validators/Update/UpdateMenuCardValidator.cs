using FluentValidation;
using Horeca.Core.Handlers.Commands.MenuCards;

namespace Horeca.Core.Validators.Update
{
    public class UpdateMenuCardValidator : AbstractValidator<EditMenuCardCommand>
    {
        public UpdateMenuCardValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
