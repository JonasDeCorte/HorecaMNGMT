using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Schedules;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Queries.Schedules
{
    public class GetScheduleByIdQuery : IRequest<ScheduleByIdDto>
    {
        public GetScheduleByIdQuery(int id, int restaurantId)
        {
            Id = id;
            RestaurantId = restaurantId;
        }

        public int Id { get; }
        public int RestaurantId { get; }
    }

    public class GetScheduleByIdQueryHandler : IRequestHandler<GetScheduleByIdQuery, ScheduleByIdDto>
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;

        public GetScheduleByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ScheduleByIdDto> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to return {object} with id: {id}", nameof(ScheduleByIdDto), request.Id);

            var restaurantSchedule = await repository.Schedules.GetScheduleById(request.Id, request.RestaurantId);
            if (restaurantSchedule is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            logger.Info("returning {@object} with id: {id}", restaurantSchedule, restaurantSchedule.Id);

            return mapper.Map<ScheduleByIdDto>(restaurantSchedule);
        }
    }
}