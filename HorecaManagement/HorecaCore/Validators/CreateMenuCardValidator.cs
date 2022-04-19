﻿using FluentValidation;
using Horeca.Core.Handlers.Commands.MenuCards;

namespace Horeca.Core.Validators
{
    public class CreateMenuCardValidator : AbstractValidator<CreateMenuCardCommand>
    {
        public CreateMenuCardValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}