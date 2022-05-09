using FluentValidation;
using Horeca.Core.Handlers.Commands.Restaurants;

namespace Horeca.Core.Validators.Create
{
    public class CreateRestaurantValidator : AbstractValidator<AddRestaurantCommand>
    {
        public CreateRestaurantValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Model.OwnerName).NotEmpty().WithMessage("Owner must be defined");
        }
    }
}