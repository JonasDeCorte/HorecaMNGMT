using FluentValidation;
using Horeca.Shared.Dtos.Accounts;

namespace HorecaCore.Validators
{
    public class CreateRoleDtoValidator : AbstractValidator<MutateRoleDto>
    {
        public CreateRoleDtoValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty().WithMessage("Name is required");
        }
    }
}