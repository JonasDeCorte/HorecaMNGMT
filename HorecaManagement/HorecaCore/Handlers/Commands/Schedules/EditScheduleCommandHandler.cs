using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Schedules;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Schedules
{
    public class EditScheduleCommand : IRequest<int>
    {
        public MutateScheduleDto Model { get; }

        public EditScheduleCommand(MutateScheduleDto model)
        {
            Model = model;
        }
    }

    public class EditScheduleCommandHandler : IRequestHandler<EditScheduleCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EditScheduleCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditScheduleCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to edit {@object} with Id: {Id}", request.Model, request.Model.Id);

            var restaurantSchedule = await repository.Schedules.GetScheduleById(request.Model.Id, request.Model.RestaurantId);

            if (restaurantSchedule is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            var checkStartTime = await repository.Schedules.CheckExistingStartTime(restaurantSchedule.Id, request.Model.ScheduleDate, request.Model.StartTime, request.Model.RestaurantId, "update");
            if (checkStartTime)
            {
                logger.Error($"Attempt to add the schedule {nameof(Schedule)} failed due to duplicate start time {nameof(request.Model.StartTime)}");
                throw new ArgumentException("duplicate start time");
            }
            restaurantSchedule = UpdateEntity(request, restaurantSchedule);

            repository.Schedules.Update(restaurantSchedule);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", restaurantSchedule, restaurantSchedule.Id);

            return restaurantSchedule.Id;
        }

        private static Schedule UpdateEntity(EditScheduleCommand request, Schedule? restaurantSchedule)
        {
            if (restaurantSchedule.StartTime != request.Model.StartTime)
            {
                restaurantSchedule.StartTime = request.Model.StartTime;
            }
            if (restaurantSchedule.EndTime != request.Model.EndTime)
            {
                restaurantSchedule.EndTime = request.Model.EndTime;
            }
            if (restaurantSchedule.ScheduleDate != request.Model.ScheduleDate)
            {
                restaurantSchedule.ScheduleDate = request.Model.ScheduleDate;
            }
            if (restaurantSchedule.Status != request.Model.Status)
            {
                restaurantSchedule.Status = request.Model.Status;
            }
            if (restaurantSchedule.Capacity != request.Model.Capacity)
            {
                restaurantSchedule.Capacity = request.Model.Capacity;
            }
            if (restaurantSchedule.AvailableSeat != request.Model.AvailableSeat)
            {
                restaurantSchedule.AvailableSeat = request.Model.AvailableSeat;
            }
            if (restaurantSchedule.RestaurantId != request.Model.RestaurantId)
            {
                restaurantSchedule.RestaurantId = request.Model.RestaurantId;
            }
            return restaurantSchedule;
        }
    }
}