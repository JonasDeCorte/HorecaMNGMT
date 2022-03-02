﻿using FluentValidation;
using Horeca.Shared.Dtos.Units;

namespace Horeca.Core.Validators
{
    public class CreateUnitDtoValidator : AbstractValidator<CreateUnitDto>
    {
        public CreateUnitDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}