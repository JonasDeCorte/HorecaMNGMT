using FluentValidation;
using Horeca.Core.Handlers.Commands.Restaurants;

namespace Horeca.Core.Validators.Update
{
    public class UpdateRestaurantValidator : AbstractValidator<EditRestaurantCommand>
    {
        public UpdateRestaurantValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
