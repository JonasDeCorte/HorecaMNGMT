using Horeca.Core.Handlers.Commands.Tables;
using Horeca.Core.Handlers.Queries.Tables;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Floorplans;
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
        /// <param name="scheduleId">ScheduleId</param>
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
        public async Task<IActionResult> Post(int scheduleId, [FromBody] MutateTableDto model)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddTableForRestaurantScheduleCommand(model, scheduleId)));
        }

        /// <summary>
        /// Adds tables for all tables within the specified floor plan object
        /// </summary>
        /// <param name="floorplanId">FloorplanId</param>
        /// <param name="restaurantId">RestaurantId</param>
        /// <param name="model">FloorplanDetailDto object</param>
        /// <returns>
        /// list of tableIds from created tables
        /// </returns>
        ///  /// <response code="201">Success creating new tables</response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [Route(RouteConstants.TableConstants.AddTablesFromFloorplan)]
        [PermissionAuthorize(nameof(Table), Permissions.Create)]
        [ProducesResponseType(typeof(TableDto), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> AddTablesFromFloorplan(int floorplanId, int restaurantId, [FromBody] FloorplanDetailDto model)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new CreateTablesFromFloorplanCommand(model, floorplanId, restaurantId)));
        }

        /// <summary>
        ///  Delete an existing Table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an existing Table</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Table), Permissions.Delete)]
        [HttpDelete]
        [Route(RouteConstants.TableConstants.Delete)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteTableCommand(id)));
        }

        /// <summary>
        /// Update an existing Table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Success updating existing Table</response>
        /// <response code="400">Bad request</response>
        [PermissionAuthorize(nameof(Table), Permissions.Update)]
        [HttpPut]
        [Route(RouteConstants.TableConstants.Update)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateTableDto model, int id, int floorplanId)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new EditTableCommand(model, id, floorplanId)));
        }
    }
}