using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Ingredients;
using MediatR;

namespace Horeca.Core.Handlers.Commands.Dishes
{
    public class AddIngredientDishCommand : IRequest<int>
    {
        public MutateIngredientByDishDto Model { get; }

        public AddIngredientDishCommand(MutateIngredientByDishDto model)
        {
            Model = model;
        }
    }

    public class CreateDishCommandHandler : IRequestHandler<AddIngredientDishCommand, int>
    {
        private readonly IUnitOfWork repository;
        private readonly IValidator<MutateIngredientDto> _validator;

        public CreateDishCommandHandler(IUnitOfWork repository, IValidator<MutateIngredientDto> validator)
        {
            this.repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(AddIngredientDishCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request.Model.Ingredient);
            var dish = repository.Dishes.GetDishIncludingDependencies(request.Model.Id);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }
            var entity = new Ingredient
            {
                Name = request.Model.Ingredient.Name,
                BaseAmount = request.Model.Ingredient.BaseAmount,
                IngredientType = request.Model.Ingredient.IngredientType,
                Unit = new Shared.Data.Entities.Unit
                {
                    Name = request.Model.Ingredient.Unit.Name,
                }
            };
            dish.Ingredients.Add(entity);
            repository.Ingredients.Add(entity);
            repository.Dishes.Update(dish);
            await repository.CommitAsync();

            return entity.Id;
        }
    }
}