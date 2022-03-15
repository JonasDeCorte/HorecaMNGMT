using Horeca.Shared.Data;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Units
{
    public class DeleteUnitCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteUnitCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteUnitCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to delete {object} with Id: {id}", nameof(Shared.Data.Entities.Unit), request.Id);

            repository.Units.Delete(request.Id);

            logger.Info("deleted {object} with Id: {id}", nameof(Shared.Data.Entities.Unit), request.Id);

            await repository.CommitAsync();

            return request.Id;
        }
    }
}