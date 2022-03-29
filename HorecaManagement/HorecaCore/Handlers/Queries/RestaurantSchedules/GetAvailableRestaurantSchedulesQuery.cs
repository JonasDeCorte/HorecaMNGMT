using Horeca.Shared.Data;
using Horeca.Shared.Dtos.RestaurantSchedules;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.RestaurantSchedules
{
    public class GetAvailableRestaurantSchedulesQuery : IRequest<List<RestaurantScheduleDto>>
    {
        public GetAvailableRestaurantSchedulesQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public int RestaurantId { get; }
    }

    public class GetAvailableRestaurantSchedulesQueryHandler : IRequestHandler<GetAvailableRestaurantSchedulesQuery, List<RestaurantScheduleDto>>
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWork repository;

        public GetAvailableRestaurantSchedulesQueryHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<List<RestaurantScheduleDto>> Handle(GetAvailableRestaurantSchedulesQuery request, CancellationToken cancellationToken)
        {
            var schedules = await repository.RestaurantSchedules.GetAvailableRestaurantSchedules(request.RestaurantId);

            logger.Info("Restaurant schedules have been retrieved {@schedules}", schedules);

            return schedules.Select(x => new RestaurantScheduleDto()
            {
                RestaurantId = x.RestaurantId,
                ScheduleId = x.Id,
                AvailableSeat = x.AvailableSeat,
                Capacity = x.Capacity,
                EndTime = x.EndTime,
                ScheduleDate = x.ScheduleDate,
                StartTime = x.StartTime,
                Status = x.Status
            }).ToList();
        }
    }
}