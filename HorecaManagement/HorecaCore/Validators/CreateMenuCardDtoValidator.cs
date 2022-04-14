using FluentValidation;
using Horeca.Core.Handlers.Commands.MenuCards;

namespace Horeca.Core.Validators
{
    public class CreateMenuCardDtoValidator : AbstractValidator<CreateMenuCardCommand>
    {
        public CreateMenuCardDtoValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}