using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Ingredients;
using MediatR;
using NLog;

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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public CreateDishCommandHandler(IUnitOfWork repository, IValidator<MutateIngredientDto> validator)
        {
            this.repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(AddIngredientDishCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to add {@object} to Dish with Id: {Id}", request.Model.Ingredient, request.Model.Id);
            var dish = repository.Dishes.GetDishIncludingDependencies(request.Model.Id);
            Ingredient entity = null;
            if (request.Model.Ingredient.Id == 0)
            {
                var result = _validator.Validate(request.Model.Ingredient);
                if (!result.IsValid)
                {
                    logger.Error("Invalid model with errors: ", result.Errors);

                    var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                    throw new InvalidRequestBodyException
                    {
                        Errors = errors
                    };
                }

                Shared.Data.Entities.Unit unit = repository.Units.Get(request.Model.Ingredient.Unit.Id);

                logger.Info("check if unit exists in database with id {id} ", request.Model.Ingredient.Unit.Id);

                entity = new Ingredient
                {
                    Name = request.Model.Ingredient.Name,
                    BaseAmount = request.Model.Ingredient.BaseAmount,
                    IngredientType = request.Model.Ingredient.IngredientType,
                    Unit = unit ?? new Shared.Data.Entities.Unit
                    {
                        Name = request.Model.Ingredient.Unit.Name,
                    },
                };
            }
            else
            {
                logger.Info("ingredients exists, get ingredient from database  {id} ", request.Model.Ingredient.Id);

                entity = repository.Ingredients.GetIngredientIncludingUnit(request.Model.Ingredient.Id);
                logger.Info("ingredients exists, get ingredient from database  {@entity} ", entity);
            }

            dish.DishIngredients.Add(new DishIngredient()
            {
                Ingredient = entity,
                Dish = dish,
            });

            repository.Dishes.Update(dish);
            await repository.CommitAsync();

            logger.Info("succes adding {@object} to dish with id {id}", entity, dish.Id);

            return entity.Id;
        }
    }
}