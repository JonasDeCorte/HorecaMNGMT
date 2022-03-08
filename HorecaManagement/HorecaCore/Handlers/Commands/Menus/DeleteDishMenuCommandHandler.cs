using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeca.Core.Handlers.Commands.Menus
{
    public class DeleteDishMenuCommand : IRequest<int>

    {
        public DeleteDishMenuDto Model { get; set; }

        public DeleteDishMenuCommand(DeleteDishMenuDto model)
        {
            Model = model;
        }
    }

    public class DeleteDishMenuCommandHandler : IRequestHandler<DeleteDishMenuCommand, int>
    {
        private readonly IUnitOfWork repository;

        public DeleteDishMenuCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteDishMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = repository.Menus.GetMenuIncludingDependencies(request.Model.MenuId);
            var dish = repository.Dishes.Get(request.Model.DishId);

            menu.Dishes.Remove(dish);
            repository.Dishes.Delete(dish.Id);

            await repository.CommitAsync();

            return request.Model.DishId;
        }
    }
}