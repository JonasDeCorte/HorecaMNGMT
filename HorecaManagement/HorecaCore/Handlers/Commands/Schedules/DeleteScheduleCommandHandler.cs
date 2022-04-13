using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Schedules
{
    public class DeleteScheduleCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteScheduleCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteScheduleCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to delete {object} with Id: {id}", nameof(Schedule), request.Id);

            repository.Schedules.Delete(request.Id);

            await repository.CommitAsync();

            logger.Info("deleted {object} with Id: {id}", nameof(Schedule), request.Id);

            return request.Id;
        }
    }
}