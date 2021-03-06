using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Floorplans
{
    public class DeleteFloorplanCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteFloorplanCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteFloorplanCommandHandler : IRequestHandler<DeleteFloorplanCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteFloorplanCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteFloorplanCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to delete {object} with Id: {id}", nameof(Floorplan), request.Id);

            await repository.Floorplans.DeleteFloorplan(request.Id);

            await repository.CommitAsync();

            logger.Info("deleted {object} with Id: {id}", nameof(Floorplan), request.Id);

            return request.Id;
        }
    }
}
