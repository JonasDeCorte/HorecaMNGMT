using Horeca.Core.Handlers.Commands.Floorplans;
using Horeca.Core.Handlers.Queries.Floorplans;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Floorplans;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Horeca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorplanController : ControllerBase
    {
        private readonly IMediator mediator;

        public FloorplanController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///  Get list of Floorplans
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving Floorplan list</response>
        /// <response code="400">Bad request</response>
        [PermissionAuthorize(nameof(Floorplan), Permissions.Read)]
        [HttpGet]
        [Route(RouteConstants.FloorplanConstants.Get)]
        [ProducesResponseType(typeof(IEnumerable<FloorplanDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get(int restaurantId)
        {
            return Ok(await mediator.Send(new GetAllFloorplansQuery(restaurantId)));
        }

        /// <summary>
        /// Create a new Floorplan
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success creating new Floorplan</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Floorplan), Permissions.Create)]
        [HttpPost]
        [Route(RouteConstants.DishConstants.Post)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateFloorplanDto model, int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new CreateFloorplanCommand(model, restaurantId)));
        }

        /// <summary>
        /// Retrieve Floorplan by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success Retrieving Floorplan by Id</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Floorplan), Permissions.Read)]
        [HttpGet]
        [Route(RouteConstants.FloorplanConstants.GetFloorplanById)]
        [ProducesResponseType(typeof(FloorplanDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id, int restaurantId)
        {
            return Ok(await mediator.Send(new GetFloorplanByIdQuery(id, restaurantId)));
        }

        /// <summary>
        ///  Delete an existing Floorplan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an existing Floorplan</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Floorplan), Permissions.Delete)]
        [HttpDelete]
        [Route(RouteConstants.FloorplanConstants.Delete)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteFloorplanCommand(id)));
        }
    }
}
