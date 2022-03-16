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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public EditDishCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditDishCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to edit {@object} with Id: {Id}", request.Model, request.Model.Id);

            var dish = repository.Dishes.Get(request.Model.Id);

            if (dish is null)
            {
                logger.Error("{Object} with Id: {id} does not exist", nameof(dish), request.Model.Id);

                throw new EntityNotFoundException("Dish does not exist");
            }

            dish.Name = request.Model.Name ?? dish.Name;
            dish.Description = request.Model.Description ?? dish.Description;
            dish.DishType = request.Model.DishType ?? dish.DishType;
            dish.Category = request.Model.Category ?? dish.Category;

            repository.Dishes.Update(dish);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", dish, dish.Id);

            return dish.Id;
        }
    }
}