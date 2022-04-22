using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Tables;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Tables
{
    public class AddTableForRestaurantScheduleCommand : IRequest<TableDto>
    {
        public AddTableForRestaurantScheduleCommand(MutateTableDto model, int scheduleId)
        {
            Model = model;
            ScheduleId = scheduleId;
        }

        public MutateTableDto Model { get; }
        public int ScheduleId { get; }
    }

    public class AddTableForRestaurantScheduleCommandHandler : IRequestHandler<AddTableForRestaurantScheduleCommand, TableDto>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddTableForRestaurantScheduleCommandHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<TableDto> Handle(AddTableForRestaurantScheduleCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            logger.Info("trying to create {object} with request: {@Id}", nameof(Table), request);
            var restaurantSchedule = repository.Schedules.Get(request.Model.ScheduleId);

            if (restaurantSchedule == null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            var table = new Table()
            {
                ScheduleId = restaurantSchedule.Id,
                Schedule = restaurantSchedule,
                Pax = request.Model.Pax,
            };
            logger.Info("adding {@object} with id {id}", table, table.Id);

            repository.Tables.Add(table);

            restaurantSchedule.AvailableSeat -= table.Pax;
            logger.Info("updating {object} with id {id}", restaurantSchedule, restaurantSchedule.Id);
            repository.Schedules.Update(restaurantSchedule);

            await repository.CommitAsync();

            return mapper.Map<TableDto>(table);
        }

        private static void ValidateModelIds(AddTableForRestaurantScheduleCommand request)
        {
            if (request.Model.ScheduleId == 0)
            {
                request.Model.ScheduleId = request.ScheduleId;
            }
        }
    }
}