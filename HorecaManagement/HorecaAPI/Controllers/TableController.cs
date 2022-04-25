using Horeca.Core.Handlers.Commands.Tables;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Tables;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Horeca.API.Controllers
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
        /// <param name="ScheduleId">ScheduleId</param>
        /// <param name="model">mutateTableDto object</param>
        /// <returns>
        ///list of added tables for each booking for the specified restaurant schedule id
        /// </returns>
        ///  /// <response code="201">Success creating new table</response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [Route(RouteConstants.TableConstants.Post)]
        [PermissionAuthorize(nameof(Table), Permissions.Create)]
        [ProducesResponseType(typeof(TableDto), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post(int ScheduleId, [FromBody] MutateTableDto model)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddTableForRestaurantScheduleCommand(model, ScheduleId)));
        }
    }
}