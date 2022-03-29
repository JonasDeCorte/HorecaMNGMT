using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.RestaurantSchedules;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.RestaurantSchedules
{
    public class AddRestaurantScheduleCommand : IRequest<int>
    {
        public MutateRestaurantScheduleDto Model { get; }

        public AddRestaurantScheduleCommand(MutateRestaurantScheduleDto model)
        {
            Model = model;
        }

        public class AddRestaurantScheduleCommandHandler : IRequestHandler<AddRestaurantScheduleCommand, int>
        {
            private readonly IUnitOfWork repository;
            private readonly IValidator<MutateRestaurantScheduleDto> validator;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public AddRestaurantScheduleCommandHandler(IUnitOfWork repository, IValidator<MutateRestaurantScheduleDto> validator)
            {
                this.repository = repository;
                this.validator = validator;
            }

            public async Task<int> Handle(AddRestaurantScheduleCommand request, CancellationToken cancellationToken)
            {
                logger.Info("trying to create {object} with request: {@Id}", nameof(RestaurantSchedule), request);

                var result = validator.Validate(request.Model);

                if (!result.IsValid)
                {
                    logger.Error("Invalid model with errors: ", result.Errors);

                    var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                    throw new InvalidRequestBodyException
                    {
                        Errors = errors
                    };
                }
                bool checkStartTime = await repository.RestaurantSchedules.CheckExistingStartTime(0, request.Model.ScheduleDate, request.Model.StartTime, request.Model.RestaurantId, "add");

                if (checkStartTime)
                {
                    logger.Error($"Attempt to add the schedule {nameof(RestaurantSchedule)} failed due to duplicate start time {nameof(request.Model.StartTime)}");
                    throw new ArgumentException("duplicate start time");
                }
                var entity = new RestaurantSchedule
                {
                    AvailableSeat = request.Model.AvailableSeat,
                    Capacity = request.Model.Capacity,
                    StartTime = request.Model.StartTime,
                    EndTime = request.Model.EndTime,
                    ScheduleDate = request.Model.ScheduleDate,
                    RestaurantId = request.Model.RestaurantId,
                    Status = request.Model.Status,
                };

                await repository.CommitAsync();

                logger.Info("adding {@object} with id {id}", entity, entity.Id);

                return entity.Id;
            }
        }
    }
}