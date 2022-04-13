using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Schedules;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Schedules
{
    public class GetAvailableSchedulesQuery : IRequest<IEnumerable<ScheduleDto>>
    {
        public GetAvailableSchedulesQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public int RestaurantId { get; }
    }

    public class GetAvailableSchedulesQueryHandler : IRequestHandler<GetAvailableSchedulesQuery, IEnumerable<ScheduleDto>>
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;

        public GetAvailableSchedulesQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ScheduleDto>> Handle(GetAvailableSchedulesQuery request, CancellationToken cancellationToken)
        {
            List<Schedule>? schedules = await repository.Schedules.GetAvailableRestaurantSchedules(request.RestaurantId);

            logger.Info("Restaurant schedules have been retrieved {@schedules}", schedules);

            return mapper.Map<IEnumerable<ScheduleDto>>(schedules);
        }
    }
}