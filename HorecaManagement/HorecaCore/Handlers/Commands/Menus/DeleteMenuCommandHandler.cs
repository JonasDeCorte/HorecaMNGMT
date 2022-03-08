using Horeca.Shared.Data;
using MediatR;

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

        public DeleteMenuCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            repository.Menus.Delete(request.Id);

            await repository.CommitAsync();

            return request.Id;
        }
    }
}