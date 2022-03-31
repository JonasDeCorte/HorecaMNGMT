using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Ingredients;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Dishes
{
    public class GetIngredientsByDishIdQuery : IRequest<DishIngredientsByIdDto>
    {
        public GetIngredientsByDishIdQuery(int dishId)
        {
            DishId = dishId;
        }

        public int DishId { get; }
    }

    public class GetIngredientsByDishIdHandler : IRequestHandler<GetIngredientsByDishIdQuery, DishIngredientsByIdDto>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper _mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetIngredientsByDishIdHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<DishIngredientsByIdDto> Handle(GetIngredientsByDishIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to return {object} with id: {id}", nameof(DishIngredientsByIdDto), request.DishId);

            var dish = await Task.FromResult(repository.Dishes.GetDishIncludingDependencies(request.DishId));
            if (dish is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            logger.Info("returning {@object} with id: {id}", dish, request.DishId);
            List<IngredientDto> ingreddto = new();
            foreach (var item in dish.DishIngredients)
            {
                ingreddto.Add(new IngredientDto()
                {
                    BaseAmount = item.Ingredient.BaseAmount,
                    Id = item.Ingredient.Id,
                    IngredientType = item.Ingredient.IngredientType,
                    Name = item.Ingredient.Name,
                    Unit = new Shared.Dtos.Units.UnitDto()
                    {
                        Name = item.Ingredient.Unit.Name,
                        Id = item.Ingredient.Unit.Id,
                    }
                });
            }
            return new DishIngredientsByIdDto()
            {
                Id = dish.Id,
                Ingredients = ingreddto
            };
        }
    }
}