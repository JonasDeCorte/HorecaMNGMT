using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Restaurants;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Restaurants
{
    public class GetRestaurantByIdQuery : IRequest<DetailRestaurantDto>
    {
        public GetRestaurantByIdQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public int RestaurantId { get; }
    }

    public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, DetailRestaurantDto>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetRestaurantByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<DetailRestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to return {object} with id: {id}", nameof(DetailRestaurantDto), request.RestaurantId);

            var restaurant = await Task.FromResult(repository.RestaurantRepository.GetRestaurantIncludingDependenciesById(request.RestaurantId));

            if (restaurant is null)
            {
                logger.Error("{object} with Id: {id} is null", nameof(restaurant), request.RestaurantId);

                throw new EntityNotFoundException("Restaurant does not exist");
            }
            logger.Info("returning {@object} with id: {id}", restaurant, request.RestaurantId);

            return mapper.Map<DetailRestaurantDto>(restaurant);
        }
    }
}