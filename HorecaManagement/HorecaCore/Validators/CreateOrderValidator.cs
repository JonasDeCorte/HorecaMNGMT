using FluentValidation;
using Horeca.Core.Handlers.Commands.Orders;

namespace Horeca.Core.Validators
{
    public class CreateOrderValidator : AbstractValidator<AddOrderCommand>

    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.Model.Dishes).NotEmpty().WithMessage("Dishes cannot be empty");
            RuleFor(x => x.Model.TableId).NotEmpty().WithMessage(" Id cannot be empty");
        }
    }
}