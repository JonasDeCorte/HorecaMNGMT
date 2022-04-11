using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.Core.Handlers.Commands.Kitchens
{
    public class ProcessOrderCommand : IRequest<int>
    {
        public ProcessOrderCommand(int orderId, int kitchenId)
        {
            OrderId = orderId;
            KitchenId = kitchenId;
        }

        public int OrderId { get; }
        public int KitchenId { get; }
    }

    public class ProcessOrderCommandHandler : IRequestHandler<ProcessOrderCommand, int>
    {
        private readonly IUnitOfWork repository;
        private readonly IApplicationDbContext context;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ProcessOrderCommandHandler(IUnitOfWork repository, IApplicationDbContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        public async Task<int> Handle(ProcessOrderCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to process order {object} with request ids: {@Id}", nameof(Order), request);

            var kitchen = await context.Kitchens.Include(x => x.Orders).SingleOrDefaultAsync(x => x.Id.Equals(request.KitchenId), cancellationToken: cancellationToken);
            if (kitchen == null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            var order = kitchen.Orders.Find(x => x.Id.Equals(request.OrderId));

            if (order == null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            logger.Info("order {object} with state: {state}", order, order.OrderState);

            order.OrderState = OrderState.Delivered;
            logger.Info("changing order {object} to  state: {state}", order, order.OrderState);

            repository.Orders.Update(order);

            logger.Info("updating order {object}", order);

            await repository.CommitAsync();

            return order.Id;
        }
    }
}