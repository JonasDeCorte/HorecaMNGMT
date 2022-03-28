using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using MediatR;
using NLog;

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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteDishCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to delete {object} with Id: {id}", nameof(Dish), request.Id);

            repository.Dishes.Delete(request.Id);

            await repository.CommitAsync();

            logger.Info("deleted {object} with Id: {id}", nameof(Dish), request.Id);

            return request.Id;
        }
    }
}