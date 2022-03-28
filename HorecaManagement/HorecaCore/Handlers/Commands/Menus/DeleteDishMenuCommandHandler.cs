using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using NLog;
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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteDishMenuCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteDishMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = repository.Menus.GetMenuIncludingDependencies(request.Model.MenuId);
            var dish = repository.Dishes.Get(request.Model.DishId);

            logger.Info("trying to delete {@object} with id {objId} from {@dish} with Id: {id}", dish, request.Model.DishId, menu, request.Model.MenuId);

            menu.Dishes.Remove(dish);
            repository.Dishes.Delete(dish.Id);

            await repository.CommitAsync();

            logger.Info("deleted {@object} with id {objId} from {@dish} with Id: {id}", dish, request.Model.DishId, menu, request.Model.MenuId);

            return request.Model.DishId;
        }
    }
}