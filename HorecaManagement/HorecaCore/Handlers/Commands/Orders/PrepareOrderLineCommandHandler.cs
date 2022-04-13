using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.Core.Handlers.Commands.Orders

{
    public class PrepareOrderLineCommand : IRequest<int>
    {
        public PrepareOrderLineCommand(int orderLineId, int orderId, int restaurantId)
        {
            OrderLineId = orderLineId;
            OrderId = orderId;
            RestaurantId = restaurantId;
        }

        public int OrderLineId { get; }
        public int OrderId { get; }
        public int RestaurantId { get; }
    }

    public class PrepareOrderLineCommandHandler : IRequestHandler<PrepareOrderLineCommand, int>
    {
        private readonly IUnitOfWork repository;
        private readonly IApplicationDbContext context;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public PrepareOrderLineCommandHandler(IUnitOfWork repository, IApplicationDbContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        public async Task<int> Handle(PrepareOrderLineCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to process order {object} with request ids: {@Id}", nameof(Order), request);

            var restaurant = await repository.Restaurants.GetRestaurantIncludingDependenciesById(request.RestaurantId);
            if (restaurant == null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            var order = restaurant.Orders.Find(x => x.Id.Equals(request.OrderId));

            if (order == null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            logger.Info("order {object} with state: {state}", order, order.OrderState);

            var orderline = order.OrderLines.Find(x => x.Id.Equals(request.OrderLineId));
            if (orderline == null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            logger.Info("order {object} with state: {state}", orderline, orderline.DishState);

            if (order.OrderState != OrderState.Prepare)
            {
                order.OrderState = OrderState.Prepare;
                context.Orders.Update(order);
                logger.Info("preparing order {object} with state: {state}", orderline, orderline.DishState);
            }

            logger.Info("orderLine {object} with state: {state}", orderline, orderline.DishState);

            if (orderline.DishState != DishState.Waiting)
            {
                logger.Info("orderLine {object} with state: {state}, has to be in state {waiting}", orderline, orderline.DishState, DishState.Waiting);
                throw new ArgumentException("Invalid DishState - should be waiting");
            }

            orderline.DishState = DishState.Preparing;

            logger.Info("started preparing orderLine {object} with state: {state}", orderline, orderline.DishState);

            context.OrderLines.Update(orderline);

            logger.Info("updating order {object}", order);

            await repository.CommitAsync();

            return orderline.Id;
        }
    }
}