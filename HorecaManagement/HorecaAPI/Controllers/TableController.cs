using Horeca.Core.Handlers.Commands.Tables;
using Horeca.Core.Handlers.Queries.Tables;
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
        ///  Get list of Tables
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving Table list</response>
        /// <response code="400">Bad request</response>
        [PermissionAuthorize(nameof(Table), Permissions.Read)]
        [HttpGet]
        [Route(RouteConstants.TableConstants.Get)]
        [ProducesResponseType(typeof(IEnumerable<TableDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get(int floorplanId)
        {
            return Ok(await mediator.Send(new GetAllTablesQuery(floorplanId)));
        }

        /// <summary>
        /// Retrieve Table by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success Retrieving Table by Id</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Table), Permissions.Read)]
        [HttpGet]
        [Route(RouteConstants.TableConstants.GetById)]
        [ProducesResponseType(typeof(TableDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id, int floorplanId)
        {
            return Ok(await mediator.Send(new GetTableByIdQuery(id, floorplanId)));
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