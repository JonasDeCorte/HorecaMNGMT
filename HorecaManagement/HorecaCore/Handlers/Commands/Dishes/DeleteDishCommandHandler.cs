using Horeca.Shared.Data;
using MediatR;

namespace Horeca.Core.Handlers.Commands.Dishes
{
    public class DeleteDishCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteDishCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteDishCommandHandler : IRequestHandler<DeleteDishCommand, int>
    {
        private readonly IUnitOfWork _repository;

        public DeleteDishCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            _repository.Dishes.Delete(request.Id);
            await _repository.CommitAsync();
            return request.Id;
        }
    }
}