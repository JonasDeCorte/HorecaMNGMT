using FluentValidation;
using Horeca.Shared.Dtos.Menus;

namespace Horeca.Core.Validators
{
    public class CreateMenuDtoValidator : AbstractValidator<MutateMenuDto>
    {
        public CreateMenuDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");

            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        }
    }
}