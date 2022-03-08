using Horeca.Shared.Data;
using MediatR;

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

        public DeleteMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteMenuCardCommand request, CancellationToken cancellationToken)
        {
            repository.MenuCards.Delete(request.Id);

            await repository.CommitAsync();

            return request.Id;
        }
    }
}