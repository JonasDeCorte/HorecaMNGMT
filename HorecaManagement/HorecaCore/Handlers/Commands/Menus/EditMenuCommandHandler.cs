﻿using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Menus;
using MediatR;

namespace Horeca.Core.Handlers.Commands.Menus
{
    public class EditMenuCommand : IRequest<int>
    {
        public MutateMenuDto Model { get; }

        public EditMenuCommand(MutateMenuDto model)
        {
            Model = model;
        }
    }

    public class EditMenuCommandHandler : IRequestHandler<EditMenuCommand, int>
    {
        private readonly IUnitOfWork repository;

        public EditMenuCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = repository.Menus.GetMenuIncludingDependencies(request.Model.Id);

            if (menu is null)
            {
                throw new EntityNotFoundException("Entity does not exist");
            }

            menu.Category = request.Model.Category ?? menu.Category;
            menu.Description = request.Model.Description ?? menu.Description;
            menu.Name = request.Model.Name ?? menu.Name;

            repository.Menus.Update(menu);
            await repository.CommitAsync();

            return request.Model.Id;
        }
    }
}