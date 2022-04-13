using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.RestaurantSchedules
{
    public class DeleteRestaurantScheduleCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteRestaurantScheduleCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteRestaurantScheduleCommandHandler : IRequestHandler<DeleteRestaurantScheduleCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteRestaurantScheduleCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteRestaurantScheduleCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to delete {object} with Id: {id}", nameof(Schedule), request.Id);

            repository.RestaurantSchedules.Delete(request.Id);

            await repository.CommitAsync();

            logger.Info("deleted {object} with Id: {id}", nameof(Schedule), request.Id);

            return request.Id;
        }
    }
}