using Horeca.Core.Handlers.Commands.RestaurantSchedules;
using Horeca.Core.Handlers.Queries.RestaurantSchedules;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.RestaurantSchedules;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HorecaAPI.Controllers
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
        /// <param name="id">Restaurant Id</param>
        /// <returns>
        /// A list of restaurant schedules results will be returned.
        /// </returns>
        /// <response code="200">A list of restaurant schedules</response>
        /// <response code="400">Bad request</response

        [HttpGet]
        [Route("All/{id}")]
        [ProducesResponseType(typeof(List<RestaurantScheduleDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetAll(int id)
        {
            return Ok(await mediator.Send(new GetAvailableRestaurantSchedulesQuery(id)));
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
        [Route("{id}")]
        [ProducesResponseType(typeof(RestaurantScheduleByIdDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await mediator.Send(new GetScheduleByIdQuery(id)));
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
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateRestaurantScheduleDto model)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddRestaurantScheduleCommand(model)));
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
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateRestaurantScheduleDto model)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new EditRestaurantScheduleCommand(model)));
        }

        /// <summary>
        ///  Delete an existing Restaurant schedule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an existing Restaurant schedule</response>
        /// <response code="400">Bad request</response
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteRestaurantScheduleCommand(id)));
        }
    }
}