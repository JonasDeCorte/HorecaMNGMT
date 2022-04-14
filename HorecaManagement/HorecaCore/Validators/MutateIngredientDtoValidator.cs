﻿using FluentValidation;
using Horeca.Core.Handlers.Commands.Ingredients;
using Horeca.Shared.Dtos.Ingredients;

namespace Horeca.Core.Validators
{
    public class MutateIngredientDtoValidator : AbstractValidator<CreateIngredientCommand>
    {
        public MutateIngredientDtoValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Model.IngredientType).NotEmpty().WithMessage("IngredientType is required");
            RuleFor(x => x.Model.BaseAmount).GreaterThan(0).WithMessage("Base amount should be greater than 0");
            RuleFor(x => x.Model.Unit).NotNull().WithMessage("Unit is required");
        }
    }
}