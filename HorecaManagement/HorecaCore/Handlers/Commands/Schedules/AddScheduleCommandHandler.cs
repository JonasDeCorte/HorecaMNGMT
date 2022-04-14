using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Schedules;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Schedules
{
    public class AddScheduleCommand : IRequest<int>
    {
        public MutateScheduleDto Model { get; }

        public AddScheduleCommand(MutateScheduleDto model)
        {
            Model = model;
        }

        public class AddScheduleCommandHandler : IRequestHandler<AddScheduleCommand, int>
        {
            private readonly IUnitOfWork repository;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public AddScheduleCommandHandler(IUnitOfWork repository)
            {
                this.repository = repository;
            }

            public async Task<int> Handle(AddScheduleCommand request, CancellationToken cancellationToken)
            {
                logger.Info("trying to create {object} with request: {@Id}", nameof(Schedule), request);

                bool checkStartTime = await repository.Schedules.CheckExistingStartTime(0, request.Model.ScheduleDate, request.Model.StartTime, request.Model.RestaurantId, "add");

                if (checkStartTime)
                {
                    logger.Error($"Attempt to add the schedule {nameof(Schedule)} failed due to duplicate start time {nameof(request.Model.StartTime)}");
                    throw new ArgumentException("duplicate start time");
                }
                var entity = new Schedule
                {
                    AvailableSeat = request.Model.AvailableSeat,
                    Capacity = request.Model.Capacity,
                    StartTime = request.Model.StartTime,
                    EndTime = request.Model.EndTime,
                    ScheduleDate = request.Model.ScheduleDate,
                    RestaurantId = request.Model.RestaurantId,
                    Status = request.Model.Status,
                };
                repository.Schedules.Add(entity);

                await repository.CommitAsync();

                logger.Info("adding {@object} with id {id}", entity, entity.Id);

                return entity.Id;
            }
        }
    }
}