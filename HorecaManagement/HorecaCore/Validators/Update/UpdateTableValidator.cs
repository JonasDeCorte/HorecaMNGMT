using FluentValidation;
using Horeca.Core.Handlers.Commands.Tables;

namespace Horeca.Core.Validators.Update
{
    public class UpdateTableValidator : AbstractValidator<EditTableCommand>
    {
        public UpdateTableValidator()
        {
            RuleFor(x => x.Model.Pax).NotEmpty().GreaterThan(0).WithMessage("amount of persons has to be larger than 0");
            RuleFor(x => x.Model.Name).NotEmpty().WithMessage("name cannot be empty");
        }
    }
}