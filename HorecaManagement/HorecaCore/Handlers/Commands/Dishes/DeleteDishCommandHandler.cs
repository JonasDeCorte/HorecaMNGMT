using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IApplicationDbContext context;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteDishCommandHandler(IUnitOfWork repository, IApplicationDbContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        public async Task<int> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to delete {object} with Id: {id}", nameof(Dish), request.Id);

            repository.Dishes.Delete(request.Id);
            CheckRelatedOrdersAndDeleteIfEmpty(request);

            await repository.CommitAsync();

            logger.Info("deleted {object} with Id: {id}", nameof(Dish), request.Id);

            return request.Id;
        }

        // calling ToList() solved the issue. src: https://stackoverflow.com/questions/604831/collection-was-modified-enumeration-operation-may-not-execute

        private void CheckRelatedOrdersAndDeleteIfEmpty(DeleteDishCommand request)
        {
            var orders = context.Orders.Include(x => x.OrderLines).ToList();
            foreach (var item in orders.ToList())
            {
                foreach (var line in item.OrderLines.ToList())
                {
                    if (line.DishId.Equals(request.Id))
                    {
                        item.OrderLines.Remove(line);
                        context.OrderLines.Remove(line);
                        if (!item.OrderLines.Any())
                        {
                            context.Orders.Remove(item);
                        }
                    }
                }
            }
        }
    }
}