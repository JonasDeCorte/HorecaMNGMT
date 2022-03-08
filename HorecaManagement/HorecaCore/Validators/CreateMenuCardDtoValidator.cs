using FluentValidation;
using Horeca.Shared.Dtos.MenuCards;

namespace HorecaCore.Validators
{
    public class CreateMenuCardDtoValidator : AbstractValidator<MutateMenuCardDto>
    {
        public CreateMenuCardDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}