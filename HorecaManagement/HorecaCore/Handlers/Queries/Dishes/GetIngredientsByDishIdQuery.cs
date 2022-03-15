using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Dishes;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();

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
                logger.Error("{object} with Id: {id} is null", nameof(dish), request.DishId);

                throw new EntityNotFoundException($"No Dish found for Id {request.DishId}");
            }

            logger.Info("returning {@object} with id: {id}", dish, request.DishId);

            return _mapper.Map<DishIngredientsByIdDto>(dish);
        }
    }
}