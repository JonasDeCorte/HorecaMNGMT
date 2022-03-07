using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using MediatR;

namespace HorecaCore.Handlers.Commands.Dishes
{
    public class CreateDishCommand : IRequest<int>
    {
        public DishDtoDetail Model { get; }

        public CreateDishCommand(DishDtoDetail model)
        {
            Model = model;
        }

        public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int>
        {
            private readonly IUnitOfWork _repository;
            private readonly IValidator<DishDtoDetail> _validator;

            public CreateDishCommandHandler(IUnitOfWork repository, IValidator<DishDtoDetail> validator)
            {
                _repository = repository;
                _validator = validator;
            }

            public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
            {
                var result = _validator.Validate(request.Model);

                if (!result.IsValid)
                {
                    var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                    throw new InvalidRequestBodyException
                    {
                        Errors = errors
                    };
                }
                var entity = new Dish
                {
                    Name = request.Model.Name,
                    Category = request.Model.Category,
                    Description = request.Model.Description,
                    DishType = request.Model.DishType,
                };

                foreach (var ingredient in request.Model.Ingredients)
                {
                    var existingIngredient = _repository.Ingredients.GetIngredientIncludingUnit(ingredient.Id);

                    if (existingIngredient is null)
                    {
                        entity.Ingredients.Add(new Ingredient()
                        {
                            Name = ingredient.Name,
                            BaseAmount = ingredient.BaseAmount,
                            IngredientType = ingredient.IngredientType,
                            Unit = new Horeca.Shared.Data.Entities.Unit()
                            {
                                Name = ingredient.Unit.Name
                            }
                        });
                    }
                    else
                    {
                        entity.Ingredients.Add(existingIngredient);
                    }
                }

                _repository.Dishes.Add(entity);
                await _repository.CommitAsync();

                return entity.Id;
            }
        }
    }
}