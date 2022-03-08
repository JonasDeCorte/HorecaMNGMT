using Horeca.Shared.Data;
using MediatR;

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

        public DeleteUnitCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            repository.Units.Delete(request.Id);
            await repository.CommitAsync();
            return request.Id;
        }
    }
}