using Horeca.Core.Handlers.Commands.Orders;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Orders;
using HorecaCore.Handlers.Queries.Orders;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(typeof(MutateOrderDto), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateOrderDto model)
        {
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
        [ProducesResponseType(typeof(IEnumerable<OrderLinesByOrderIdDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetByBookingNo([FromRoute] int TableId)
        {
            return Ok(await mediator.Send(new GetOrderLinesByOrderIdQuery(TableId)));
        }
    }
}