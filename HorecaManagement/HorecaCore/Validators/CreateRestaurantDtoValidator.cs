using FluentValidation;
using Horeca.Shared.Dtos.Restaurants;

namespace Horeca.Core.Validators
{
    public class CreateRestaurantDtoValidator : AbstractValidator<MutateRestaurantDto>
    {
        public CreateRestaurantDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.OwnerName).NotEmpty().WithMessage("Owner must be defined");


        }
    }
}
