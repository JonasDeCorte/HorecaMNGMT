using Horeca.Core.Handlers.Commands.Orders;
using Horeca.Core.Handlers.Queries.Orders;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Add a new Order to the database.
        /// </summary>
        /// <param name="MutateOrderDto">MutateOrderDto object</param>
        /// <returns>
        /// New Order
        /// /// </returns>
        ///  /// <response code="201">Success creating new Order</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(Order), Permissions.Create)]
        [HttpPost]
        [Route(RouteConstants.OrderConstants.Post)]
        [ProducesResponseType(typeof(MutateOrderDto), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post(int tableId, [FromBody] MutateOrderDto model)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddOrderCommand(model, tableId)));
        }

        /// <summary>
        /// Retrieve order lines list from the selected table
        /// </summary>
        /// <param name="tableId">Table Id</param>
        /// <returns>
        /// Relevant order lines will be returned based on the table id
        /// </returns>
        /// <response code="200">Success retrieving order lines list</response>
        /// <response code="400">Bad request</response>
        [PermissionAuthorize(nameof(Order), Permissions.Read)]
        [HttpGet]
        [Route(RouteConstants.OrderConstants.Get)]
        [ProducesResponseType(typeof(IEnumerable<GetOrderLinesByTableIdDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetByTableId(int tableId)
        {
            return Ok(await mediator.Send(new GetOrderLinesByTableIdQuery(tableId)));
        }

        /// <summary>
        /// Retrieve orders list
        /// </summary>
        /// <param name="restaurantId">restaurant Id</param>
        /// <returns>
        /// Relevant order lines will be returned based on the table id
        /// </returns>
        /// <response code="200">Success retrieving order lines list</response>
        /// <response code="400">Bad request</response>
        [PermissionAuthorize(nameof(Order), Permissions.Read)]
        [HttpGet]
        [Route(RouteConstants.OrderConstants.GetOrderState)]
        [ProducesResponseType(typeof(IEnumerable<OrderDtoDetail>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetOrdersByOrderStateQuery(int restaurantId, OrderState orderState)
        {
            return Ok(await mediator.Send(new GetOrdersByOrderStateQuery(restaurantId, orderState)));
        }

        /// <summary>
        /// kitchen uses this to notify an order has been completed and is ready to be delivered
        /// </summary>
        /// <param name="kitchenId">kitchen Id</param>
        /// <param name="orderId">order id </param>
        /// <returns>
        /// returns the orderId from the order that has been completed
        /// </returns>
        /// <response code="200">Success returning the orderId </response>
        /// <response code="400">Bad request</response>
        ///
        [PermissionAuthorize(nameof(Order), Permissions.Update)]
        [HttpPut]
        [Route(RouteConstants.OrderConstants.DeliverOrder)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeliverOrderCommand(int restaurantId, int orderId)
        {
            return Ok(await mediator.Send(new DeliverOrderCommand(orderId, restaurantId)));
        }

        /// <summary>
        /// kitchen uses this to notify an orderline is being prepared
        /// </summary>
        /// <param name="kitchenId">kitchen Id</param>
        /// <param name="orderId">order id </param>
        /// <returns>
        /// returns the orderline Id from the orderline which is being prepared
        /// </returns>
        /// <response code="200">Success returning the orderLineId </response>
        /// <response code="400">Bad request</response>
        [PermissionAuthorize(nameof(Order), Permissions.Update)]
        [HttpPut]
        [Route(RouteConstants.OrderConstants.PrepareOrderLine)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> PrepareOrderLine(int orderLineId, int restaurantId, int orderId)
        {
            return Ok(await mediator.Send(new PrepareOrderLineCommand(orderLineId, orderId, restaurantId)));
        }

        /// <summary>
        /// kitchen uses this to notify an orderline is ready
        /// </summary>
        /// <param name="kitchenId">kitchen Id</param>
        /// <param name="orderId">order id </param>
        /// <returns>
        /// returns the orderline Id from the orderline which is ready
        /// </returns>
        /// <response code="200">Success returning the orderLineId </response>
        /// <response code="400">Bad request</response>
        ///
        [PermissionAuthorize(nameof(Order), Permissions.Update)]
        [HttpPut]
        [Route(RouteConstants.OrderConstants.ReadyOrderLine)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> ReadyOrderLine(int orderLineId, int restaurantId, int orderId)
        {
            return Ok(await mediator.Send(new ReadyOrderLineCommand(orderLineId, orderId, restaurantId)));
        }
    }
}