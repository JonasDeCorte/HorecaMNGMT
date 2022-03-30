using Horeca.Core.Handlers.Commands.Tables;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Tables;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HorecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly IMediator mediator;

        public TableController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Adds tables for all the bookings with the specified restaurant schedule Id
        /// </summary>
        /// <param name="restaurantScheduleId">restaurantScheduleId</param>
        /// <returns>
        ///list of added tables for each booking for the specified restaurant schedule id
        /// </returns>
        ///  /// <response code="201">Success creating new table</response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [Route("{restaurantScheduleId}")]
        [ProducesResponseType(typeof(IEnumerable<TableDto>), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromRoute] int restaurantScheduleId)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddTablesForRestaurantScheduleCommand(restaurantScheduleId)));
        }
    }
}