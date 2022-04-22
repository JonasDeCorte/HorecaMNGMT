using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Menus
{
    public class EditMenuCommand : IRequest<int>
    {
        public MutateMenuDto Model { get; }
        public int Id { get; }
        public int RestaurantId { get; }

        public EditMenuCommand(MutateMenuDto model, int id, int restaurantId)
        {
            Model = model;
            Id = id;
            RestaurantId = restaurantId;
        }
    }

    public class EditMenuCommandHandler : IRequestHandler<EditMenuCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EditMenuCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditMenuCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            logger.Info("trying to edit {object} with Id: {Id}", request.Model, request.Model.Id);
            var menu = await repository.Menus.GetMenuById(request.Model.Id, request.Model.RestaurantId);

            if (menu is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            menu.Category = request.Model.Category ?? menu.Category;
            menu.Description = request.Model.Description ?? menu.Description;
            menu.Name = request.Model.Name ?? menu.Name;
            if (menu.Price != request.Model.Price)
            {
                menu.Price = request.Model.Price;
            }

            repository.Menus.Update(menu);
            await repository.CommitAsync();

            logger.Info("updated {@object} with Id: {id}", menu, menu.Id);

            return menu.Id;
        }

        private static void ValidateModelIds(EditMenuCommand request)
        {
            if (request.Model.RestaurantId == 0)
            {
                request.Model.RestaurantId = 0;
            }
            if (request.Model.Id == 0)
            {
                request.Model.Id = request.Id;
            }
        }
    }
}