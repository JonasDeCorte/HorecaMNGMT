using FluentValidation;
using Horeca.Shared.Dtos.Tables;

namespace Horeca.Core.Validators
{
    public class CreateTableDtoValidator : AbstractValidator<CreateTableDto>
    {
        public CreateTableDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.AmountOfPeople).GreaterThan(0).NotEmpty().WithMessage("Amount of people must be greater than 0");
        }
    }
}