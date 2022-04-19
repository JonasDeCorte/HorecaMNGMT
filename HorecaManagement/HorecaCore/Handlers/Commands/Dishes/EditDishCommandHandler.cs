using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Dishes;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Dishes
{
    public class EditDishCommand : IRequest<int>
    {
        public MutateDishDto Model { get; }

        public EditDishCommand(MutateDishDto model)
        {
            Model = model;
        }
    }

    public class EditDishCommandHandler : IRequestHandler<EditDishCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EditDishCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditDishCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to edit {@object} with Id: {Id}", request.Model, request.Model.Id);

            var dish = await repository.Dishes.GetDishById(request.Model.Id, request.Model.RestaurantId);

            if (dish is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            dish.Name = request.Model.Name ?? dish.Name;
            dish.Description = request.Model.Description ?? dish.Description;
            dish.DishType = request.Model.DishType ?? dish.DishType;
            dish.Category = request.Model.Category ?? dish.Category;
            if (dish.Price != request.Model.Price)
            {
                dish.Price = request.Model.Price;
            }
            repository.Dishes.Update(dish);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", dish, dish.Id);

            return dish.Id;
        }
    }
}