using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.RestaurantSchedules;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.RestaurantSchedules
{
    public class GetScheduleByIdQuery : IRequest<RestaurantScheduleByIdDto>
    {
        public GetScheduleByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class GetScheduleByIdQueryHandler : IRequestHandler<GetScheduleByIdQuery, RestaurantScheduleByIdDto>
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;

        public GetScheduleByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<RestaurantScheduleByIdDto> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to return {object} with id: {id}", nameof(RestaurantScheduleByIdDto), request.Id);

            var restaurantSchedule = repository.RestaurantSchedules.Get(request.Id);
            var restaurant = repository.Restaurants.Get(restaurantSchedule.RestaurantId);
            if (restaurantSchedule is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            logger.Info("returning {@object} with id: {id}", restaurantSchedule, restaurantSchedule.Id);

            restaurantSchedule.Restaurant = restaurant;
            restaurantSchedule.RestaurantId = restaurant.Id;

            return mapper.Map<RestaurantScheduleByIdDto>(restaurantSchedule);
        }
    }
}