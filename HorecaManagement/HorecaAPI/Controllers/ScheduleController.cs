using Horeca.Core.Handlers.Commands.Schedules;
using Horeca.Core.Handlers.Queries.Schedules;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Schedules;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Horeca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IMediator mediator;

        public ScheduleController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get all existing restaurant schedules from the database
        /// </summary>
        /// <param name="restaurantId">Restaurant Id</param>
        /// <returns>
        /// A list of restaurant schedules results will be returned.
        /// </returns>
        /// <response code="200">A list of restaurant schedules</response>
        /// <response code="400">Bad request</response

        [HttpGet]
        [Route("Restaurant/{restaurantId}")]
        [PermissionAuthorize(nameof(Schedule), Permissions.Read)]
        [ProducesResponseType(typeof(List<ScheduleDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetAll([FromRoute] int restaurantId)
        {
            return Ok(await mediator.Send(new GetAvailableSchedulesQuery(restaurantId)));
        }

        /// <summary>
        /// Retrieve a specific restaurant schedule information based on the Schedule ID given.
        /// </summary>
        /// <param name="id">Schedule Id</param>
        /// <returns>
        /// A restaurant schedule information will be returned
        /// </returns>
        /// <response code="200">A restaurant schedule object</response>
        /// <response code="400">Bad request</response

        [HttpGet]
        [Route("{id}/Restaurant/{restaurantId}")]
        [PermissionAuthorize(nameof(Schedule), Permissions.Read)]
        [ProducesResponseType(typeof(ScheduleByIdDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get([FromRoute] int id, [FromRoute] int restaurantId)
        {
            return Ok(await mediator.Send(new GetScheduleByIdQuery(id, restaurantId)));
        }

        /// <summary>
        /// Add a new schedule for a restaurant to the database. This also checks for duplicate session start time.
        /// </summary>
        /// <param name="MutateRestaurantScheduleDto">MutateRestaurantScheduleDto object</param>
        /// <returns>
        /// No content response which means that a new schedule object has been added to the database.
        /// Otherwise, adding new schedule for a restaurant process unable to be conducted.
        /// </returns>
        /// <response code="204">No content</response>
        /// <response code="400">Bad request</
        [HttpPost]
        [Route("Restaurant/{restaurantId}")]
        [PermissionAuthorize(nameof(Schedule), Permissions.Create)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateScheduleDto model, [FromRoute] int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddScheduleCommand(model, restaurantId)));
        }

        /// <summary>
        /// Update an existing restaurant schedule info in the database. This checks for duplicate start time also.
        /// </summary
        /// <param name="MutateRestaurantScheduleDto">MutateRestaurantScheduleDto Object</param>
        /// <returns>
        /// An updated schedule info will be returned.
        /// Otherwise, here are the following why the updating existing schedule info process unable to be conducted :
        /// If the schedule name change to existing data and
        /// there is another conflict happens when 2 same processes conducting simultaneously.
        /// </returns>
        /// <response code="200">Success updating existing restaurant</response>
        /// <response code="400">Bad request</response>
        [HttpPut]
        [Route("{id}/Restaurant/{restaurantId}")]
        [PermissionAuthorize(nameof(Schedule), Permissions.Update)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateScheduleDto model, [FromRoute] int restaurantId, [FromRoute] int id)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new EditScheduleCommand(model, id, restaurantId)));
        }

        /// <summary>
        ///  Delete an existing Restaurant schedule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an existing Restaurant schedule</response>
        /// <response code="400">Bad request</response
        [HttpDelete]
        [Route("{id}/Restaurant/{restaurantId}")]
        [PermissionAuthorize(nameof(Schedule), Permissions.Delete)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteScheduleCommand(id)));
        }
    }
}