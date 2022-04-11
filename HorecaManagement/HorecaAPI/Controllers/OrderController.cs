using Horeca.Core.Handlers.Commands.Kitchens;
using Horeca.Core.Handlers.Commands.Orders;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Orders;
using HorecaCore.Handlers.Queries.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HorecaAPI.Controllers
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
        [HttpPost]
        [Route("Table/{TableId}/Order/")]
        [ProducesResponseType(typeof(MutateOrderDto), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromRoute] int TableId, [FromBody] MutateOrderDto model)
        {
            model.TableId = TableId;
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddOrderCommand(model)));
        }

        /// <summary>
        /// Retrieve order lines list from the selected table
        /// </summary>
        /// <param name="TableId">Table Id</param>
        /// <returns>
        /// Relevant order lines will be returned based on the table id
        /// </returns>
        /// <response code="200">Success retrieving order lines list</response>
        /// <response code="400">Bad request</response>
        [HttpGet]
        [Route("Table/{TableId}/Details")]
        [ProducesResponseType(typeof(IEnumerable<GetOrderLinesByTableIdDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetByTableId([FromRoute] int TableId)
        {
            return Ok(await mediator.Send(new GetOrderLinesByTableIdQuery(TableId)));
        }

        /// <summary>
        /// kitchen uses this to notify an order has been completed
        /// </summary>
        /// <param name="kitchenId">kitchen Id</param>
        /// <param name="orderId">order id </param>
        /// <returns>
        /// returns the orderId from the order that has been completed
        /// </returns>
        /// <response code="200">Success returning the orderId </response>
        /// <response code="400">Bad request</response>
        [HttpPut]
        [Route("Kitchen/{kitchenId}/Order/{orderId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeliverOrderCommand([FromRoute] int kitchenId, [FromRoute] int orderId)
        {
            return Ok(await mediator.Send(new DeliverOrderCommand(orderId, kitchenId)));
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
        [HttpPut]
        [Route("Kitchen/{kitchenId}/Order/{orderId}/OrderLine/{orderLineId}/Prepare")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> PrepareOrderLine([FromRoute] int orderLineId, [FromRoute] int kitchenId, [FromRoute] int orderId)
        {
            return Ok(await mediator.Send(new PrepareOrderLineCommand(orderLineId, orderId, kitchenId)));
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
        [HttpPut]
        [Route("Kitchen/{kitchenId}/Order/{orderId}/OrderLine/{orderLineId}/Ready")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> ReadyOrderLine([FromRoute] int orderLineId, [FromRoute] int kitchenId, [FromRoute] int orderId)
        {
            return Ok(await mediator.Send(new ReadyOrderLineCommand(orderLineId, orderId, kitchenId)));
        }
    }
}