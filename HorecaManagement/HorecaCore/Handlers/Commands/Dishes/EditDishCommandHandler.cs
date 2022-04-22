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
        public int Id { get; }
        public int RestaurantId { get; }

        public EditDishCommand(MutateDishDto model, int id, int restaurantId)
        {
            Model = model;
            Id = id;
            RestaurantId = restaurantId;
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
            ValidateModelIds(request);
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

        private static void ValidateModelIds(EditDishCommand request)
        {
            if (request.Model.Id == 0)
            {
                request.Model.Id = request.Id;
            }
            if (request.Model.RestaurantId == 0)
            {
                request.Model.RestaurantId = request.RestaurantId;
            }
        }
    }
}