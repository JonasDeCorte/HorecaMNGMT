using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Tables
{
    public class DeleteTableCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteTableCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteTableCommandHandler : IRequestHandler<DeleteTableCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteTableCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to delete {object} with Id: {id}", nameof(Table), request.Id);

            repository.Tables.Delete(request.Id);

            await repository.CommitAsync();

            logger.Info("deleted {object} with Id: {id}", nameof(Table), request.Id);

            return request.Id;
        }
    }
}
