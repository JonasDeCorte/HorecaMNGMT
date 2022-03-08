﻿using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Dishes;
using MediatR;

namespace Horeca.Core.Handlers.Commands.Dishes
{
    public class EditIngredientDishCommand : IRequest<int>
    {
        public MutateIngredientByDishDto Model { get; }

        public EditIngredientDishCommand(MutateIngredientByDishDto model)
        {
            Model = model;
        }
    }

    public class EditIngredientDishCommandHandler : IRequestHandler<EditIngredientDishCommand, int>
    {
        private readonly IUnitOfWork _repository;

        public EditIngredientDishCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(EditIngredientDishCommand request, CancellationToken cancellationToken)
        {
            var dish = _repository.Dishes.GetDishIncludingDependencies(request.Model.Id);

            if (dish is null)
            {
                throw new EntityNotFoundException("Dish does not exist");
            }
            var ingredient = dish.Ingredients.SingleOrDefault(x => x.Id == request.Model.Ingredient.Id);
            if (ingredient is null)
            {
                throw new EntityNotFoundException("Ingredient does not exist");
            }

            ingredient.Name = request.Model.Ingredient.Name ?? ingredient.Name;
            if (request.Model.Ingredient.BaseAmount != ingredient.BaseAmount)
                ingredient.BaseAmount = request.Model.Ingredient.BaseAmount;
            ingredient.IngredientType = request.Model.Ingredient.IngredientType ?? ingredient.IngredientType;
            ingredient.Unit = request.Model.Ingredient.Unit ?? ingredient.Unit;

            _repository.Ingredients.Update(ingredient);
            _repository.Dishes.Update(dish);

            await _repository.CommitAsync();

            return dish.Id;
        }
    }
}