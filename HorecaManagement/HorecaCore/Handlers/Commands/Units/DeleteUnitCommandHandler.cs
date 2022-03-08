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
        private readonly IUnitOfWork _repository;

        public DeleteUnitCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            _repository.Units.Delete(request.Id);
            await _repository.CommitAsync();
            return request.Id;
        }
    }
}