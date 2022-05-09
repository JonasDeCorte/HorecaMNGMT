using FluentValidation;
using Horeca.Core.Handlers.Commands.Floorplans;

namespace Horeca.Core.Validators.Create
{
    public class CreateFloorplanValidator : AbstractValidator<CreateFloorplanCommand>
    {
        public CreateFloorplanValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
