using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Ingredients;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Ingredients
{
    public class GetAllIngredientsByRestaurantIdQuery : IRequest<IEnumerable<IngredientDto>>
    {
        public GetAllIngredientsByRestaurantIdQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public int RestaurantId { get; }
    }

    public class GetAllIngredientsByRestaurantIdQueryHandler : IRequestHandler<GetAllIngredientsByRestaurantIdQuery, IEnumerable<IngredientDto>>

    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllIngredientsByRestaurantIdQueryHandler(IUnitOfWork repository, IMapper mapper)

        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<IngredientDto>> Handle(GetAllIngredientsByRestaurantIdQuery request, CancellationToken cancellationToken)

        {
            var entities = await repository.Ingredients.GetAllIncludingUnit(request.RestaurantId);
            logger.Info("{amount} of {nameof} have been returned", entities.Count(), nameof(IngredientDto));

            return mapper.Map<IEnumerable<IngredientDto>>(entities);
        }
    }
}