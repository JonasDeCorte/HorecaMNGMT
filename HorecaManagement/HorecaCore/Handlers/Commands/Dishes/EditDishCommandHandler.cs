using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using MediatR;

namespace HorecaCore.Handlers.Commands.Dishes
{
    public class EditDishCommand : IRequest<int>
    {
        public DishDtoDetail Model { get; }

        public EditDishCommand(DishDtoDetail model)
        {
            Model = model;
        }
    }

    public class EditDishCommandHandler : IRequestHandler<EditDishCommand, int>
    {
        private readonly IUnitOfWork _repository;

        public EditDishCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(EditDishCommand request, CancellationToken cancellationToken)
        {
            // entity as it currently exists in the db
            var dish = _repository.Dishes.GetDishIncludingDependencies(request.Model.Id); // tracked state unchanged
            Console.WriteLine("===================================================");
            if (dish is null)
            {
                throw new EntityNotFoundException("Dish does not exist");
            }

            dish.Name = request.Model.Name ?? dish.Name;
            dish.Description = request.Model.Description ?? dish.Description;
            dish.DishType = request.Model.DishType ?? dish.DishType;
            dish.Category = request.Model.Category ?? dish.Category;

            // zonder deze foreach blijft de state op unchanged voor Ingredients & units, toevoegen van ingredients met default id resulteert in de error:
            // The property 'Ingredient.Id' has a temporary value while attempting to change the entity's state to 'Modified'.
            // Either set a permanent value explicitly, or ensure that the database is configured to generate values for this property.
            dish.Ingredients = request.Model.Ingredients ?? dish.Ingredients;
            foreach (var ingredient in dish.Ingredients)
            {
                _repository.Ingredients.Update(ingredient); // state modified
                _repository.Units.Update(ingredient.Unit); // state modified
            }

            _repository.Dishes.Update(dish);

            await _repository.CommitAsync();

            return dish.Id;
        }
    }
}