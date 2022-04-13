using FluentValidation;
using Horeca.Shared.Dtos.Orders;

namespace Horeca.Core.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<MutateOrderDto>

    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.Dishes).NotEmpty().WithMessage("Dishes cannot be empty");
            RuleFor(x => x.TableId).NotEmpty().WithMessage(" Id cannot be empty");
        }
    }
}