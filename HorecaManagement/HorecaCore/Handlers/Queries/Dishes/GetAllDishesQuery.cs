using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Dishes;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Dishes
{
    public class GetAllDishesQuery : IRequest<IEnumerable<DishDto>>
    {
        public GetAllDishesQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public int RestaurantId { get; }
    }

    public class GetAllDishesQueryHandler : IRequestHandler<GetAllDishesQuery, IEnumerable<DishDto>>

    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllDishesQueryHandler(IUnitOfWork repository, IMapper mapper)

        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DishDto>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            var entities = await repository.Dishes.GetAllDishes(request.RestaurantId);

            logger.Info("{amount} of {nameof} have been returned", entities.Count(), nameof(DishDto));

            return mapper.Map<IEnumerable<DishDto>>(entities);
        }
    }
}