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

        public GetScheduleByIdQueryHandler(IUnitOfWork repository)
        {
            this.repository = repository;
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

            return new RestaurantScheduleByIdDto()
            {
                AvailableSeat = restaurantSchedule.AvailableSeat,
                Capacity = restaurantSchedule.Capacity,
                EndTime = restaurantSchedule.EndTime,
                RestaurantId = restaurant.Id,
                RestaurantName = restaurant.Name,
                ScheduleDate = restaurantSchedule.ScheduleDate,
                StartTime = restaurantSchedule.StartTime,
                Status = restaurantSchedule.Status,
                ScheduleId = restaurantSchedule.Id
            };
        }
    }
}