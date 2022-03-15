using FluentValidation;
using Horeca.Shared.Dtos.Reservation;

namespace Horeca.Core.Validators
{
    public class CreateReservationDtoValidator : AbstractValidator<MutateReservationDto>
    {
        public CreateReservationDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.AmountOfPeople).GreaterThan(0).NotEmpty().WithMessage("Amount of people must be greater than 0");
            RuleFor(x => x.TableId).NotEmpty().WithMessage("TableId cannot empty");
            RuleFor(x => x.DateOfReservation).GreaterThan(DateTime.Today.AddDays(-1)).NotEmpty().WithMessage("Date has to be in the present");
        }
    }
}