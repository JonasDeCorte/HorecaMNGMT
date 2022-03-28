using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Restaurants;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Restaurants
{
    public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
    {
        public GetAllRestaurantsQuery()
        {
        }
    }

    public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllRestaurantsQueryHandler(IUnitOfWork repository)

        {
            this.repository = repository;
        }

        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var result = await Task.FromResult(repository.Restaurants.GetAll().Select(x => new RestaurantDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList());

            logger.Info("{amount} of {nameof} have been returned", result.Count, nameof(RestaurantDto));

            return result;
        }
    }
}