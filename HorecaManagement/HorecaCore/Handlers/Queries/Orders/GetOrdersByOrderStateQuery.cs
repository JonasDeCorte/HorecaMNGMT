using AutoMapper;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Orders;
using MediatR;
using NLog;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.Core.Handlers.Queries.Orders
{
    public class GetOrdersByOrderStateQuery : IRequest<List<OrderDtoDetail>>
    {
        public GetOrdersByOrderStateQuery(int restaurantId, OrderState orderState)
        {
            RestaurantId = restaurantId;
            OrderState = orderState;
        }

        public int RestaurantId { get; }
        public OrderState OrderState { get; }
    }

    public class GetOrdersByOrderStateQueryHandler : IRequestHandler<GetOrdersByOrderStateQuery, List<OrderDtoDetail>>
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper mapper;
        private readonly IUnitOfWork repository;

        public GetOrdersByOrderStateQueryHandler(IMapper mapper, IUnitOfWork repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<List<OrderDtoDetail>> Handle(GetOrdersByOrderStateQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to return {object} with request: {@req}", nameof(OrderDtoDetail), request);

            var restaurant = await repository.Restaurants.GetRestaurantByIdWithOrdersWithOrderState(request.RestaurantId, request.OrderState);

            if (restaurant == null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            if (restaurant.Orders.Count == 0)
            {
                logger.Error("no orders have been found with this status");
                return new List<OrderDtoDetail>();
            }

            logger.Info("returning {@object} with state: {state }", restaurant.Orders, request.OrderState);

            return mapper.Map<List<OrderDtoDetail>>(restaurant.Orders);
        }
    }
}