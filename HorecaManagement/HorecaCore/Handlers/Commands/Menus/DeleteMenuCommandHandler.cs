using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Menus
{
    public class DeleteMenuCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteMenuCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteMenuCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to delete {object} with Id: {id}", nameof(Menu), request.Id);

            repository.Menus.Delete(request.Id);

            await repository.CommitAsync();

            logger.Info("deleted {object} with Id: {id}", nameof(Menu), request.Id);

            return request.Id;
        }
    }
}