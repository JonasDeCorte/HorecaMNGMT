using FluentValidation;
using Horeca.Core.Handlers.Commands.Restaurants;

namespace Horeca.Core.Validators
{
    public class CreateRestaurantDtoValidator : AbstractValidator<AddRestaurantCommand>
    {
        public CreateRestaurantDtoValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Model.OwnerName).NotEmpty().WithMessage("Owner must be defined");
        }
    }
}