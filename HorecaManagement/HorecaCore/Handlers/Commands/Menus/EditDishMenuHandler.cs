﻿using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Menus;
using MediatR;

namespace Horeca.Core.Handlers.Commands.Menus
{
    public class EditDishMenuCommand : IRequest<int>
    {
        public MutateDishMenuDto Model { get; }

        public EditDishMenuCommand(MutateDishMenuDto model)
        {
            Model = model;
        }
    }

    public class EditIngredientDishCommandHandler : IRequestHandler<EditDishMenuCommand, int>
    {
        private readonly IUnitOfWork _repository;

        public EditIngredientDishCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(EditDishMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = _repository.Menus.GetMenuIncludingDependencies(request.Model.Id);

            if (menu is null)
            {
                throw new EntityNotFoundException("menu does not exist");
            }
            var dish = menu.Dishes.SingleOrDefault(x => x.Id == request.Model.Dish.Id);

            if (dish is null)
            {
                throw new EntityNotFoundException("dish does not exist");
            }

            dish.Name = request.Model.Dish.Name ?? dish.Name;
            dish.DishType = request.Model.Dish.DishType ?? dish.DishType;
            dish.Category = request.Model.Dish.Category ?? dish.Category;
            dish.Description = request.Model.Dish.Description ?? dish.Description;

            _repository.Dishes.Update(dish);
            _repository.Menus.Update(menu);

            await _repository.CommitAsync();

            return dish.Id;
        }
    }
}