﻿using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Horeca.Core.Handlers.Commands.Roles
{
    public class CreateRoleCommand : IRequest<string>
    {
        public CreateRoleCommand(MutateRoleDto model)
        {
            Model = model;
        }

        public MutateRoleDto Model { get; }
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, string>
    {
        private readonly IValidator<MutateRoleDto> validator;
        private readonly RoleManager<IdentityRole> roleManager;

        public CreateRoleCommandHandler(IValidator<MutateRoleDto> validator, RoleManager<IdentityRole> roleManager)
        {
            this.validator = validator;
            this.roleManager = roleManager;
        }

        public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = validator.Validate(request.Model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }
            var identityRole = new IdentityRole
            {
                Name = request.Model.RoleName
            };

            if (!await roleManager.RoleExistsAsync(identityRole.Name))
                await roleManager.CreateAsync(new IdentityRole(identityRole.Name));

            return identityRole.Id;
        }
    }
}