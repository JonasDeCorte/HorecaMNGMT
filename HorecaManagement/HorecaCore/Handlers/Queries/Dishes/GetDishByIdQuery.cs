using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Dishes;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Dishes
{
    public class GetDishByIdQuery : IRequest<DishDto>
    {
        public int DishId { get; }
        public int RestaurantId { get; }

        public GetDishByIdQuery(int dishId, int restaurantId)
        {
            DishId = dishId;
            RestaurantId = restaurantId;
        }

        public class GetDishByIdQueryHandler : IRequestHandler<GetDishByIdQuery, DishDto>
        {
            private readonly IUnitOfWork repository;
            private readonly IMapper mapper;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public GetDishByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
            {
                logger.Info("trying to return {object} with id: {id}", nameof(DishDto), request.DishId);

                var dish = await repository.Dishes.GetDishById(request.DishId, request.RestaurantId);

                if (dish is null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }
                logger.Info("returning {@object} with id: {id}", dish, request.DishId);

                return mapper.Map<DishDto>(dish);
            }
        }
    }
}