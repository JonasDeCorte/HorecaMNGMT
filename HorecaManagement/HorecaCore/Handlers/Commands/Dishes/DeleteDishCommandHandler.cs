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
        private readonly IUnitOfWork repository;

        public DeleteDishCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            repository.Dishes.Delete(request.Id);
            await repository.CommitAsync();
            return request.Id;
        }
    }
}