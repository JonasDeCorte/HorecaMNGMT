using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Tables
{
    public class DeleteTableCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteTableCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteTableCommandHandler : IRequestHandler<DeleteTableCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteTableCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to delete {object} with Id: {id}", nameof(Table), request.Id);
            var orders = await repository.Orders.GetOrdersByTable(request.Id);
            if (!orders.Any())
            {
                logger.Info("No ORDERS with table Id: {id}", request.Id);
            }
            else
            {
                DeleteOrders(orders);
            }

            repository.Tables.Delete(request.Id);

            await repository.CommitAsync();

            logger.Info("deleted {object} with Id: {id}", nameof(Table), request.Id);

            return request.Id;
        }

        private void DeleteOrders(List<Order> orders)
        {
            foreach (var item in orders)
            {
                repository.Orders.Delete(item.Id);
                logger.Info("Deleted order with Id: {id}", item.Id);
            }
        }
    }
}