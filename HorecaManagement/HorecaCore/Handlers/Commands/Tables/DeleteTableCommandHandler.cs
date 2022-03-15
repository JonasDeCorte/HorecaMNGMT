using Horeca.Shared.Data;
using MediatR;

namespace Horeca.Core.Handlers.Commands.Tables
{
    public class DeleteTableCommand : IRequest<int>
    {
        public DeleteTableCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class DeleteTableCommandHandler : IRequestHandler<DeleteTableCommand, int>
    {
        private readonly IUnitOfWork repository;

        public DeleteTableCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
        {
            repository.Tables.Delete(request.Id);
            await repository.CommitAsync();
            return request.Id;
        }
    }
}