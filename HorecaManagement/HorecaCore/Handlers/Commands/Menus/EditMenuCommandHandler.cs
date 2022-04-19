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

        public EditMenuCommand(MutateMenuDto model)
        {
            Model = model;
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
            logger.Info("trying to edit {object} with Id: {Id}", request.Model, request.Model.Id);

            var menu = await repository.Menus.GetMenuIncludingDependencies(request.Model.Id, request.Model.RestaurantId);

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
    }
}