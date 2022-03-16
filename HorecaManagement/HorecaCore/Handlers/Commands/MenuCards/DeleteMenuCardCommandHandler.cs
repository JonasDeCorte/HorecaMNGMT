using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class DeleteMenuCardCommand : IRequest<int>
    {
        public DeleteMenuCardCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class DeleteMenuCardCommandHandler : IRequestHandler<DeleteMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteMenuCardCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to delete {object} with Id: {id}", nameof(MenuCard), request.Id);

            repository.MenuCards.Delete(request.Id);

            await repository.CommitAsync();
            logger.Info("deleted {object} with Id: {id}", nameof(MenuCard), request.Id);

            return request.Id;
        }
    }
}