using FluentValidation;
using Horeca.Core.Handlers.Commands.Orders;

namespace Horeca.Core.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<AddOrderCommand>

    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.Model.Dishes).NotEmpty().WithMessage("Dishes cannot be empty");
            RuleFor(x => x.Model.TableId).NotEmpty().WithMessage(" Id cannot be empty");
        }
    }
}