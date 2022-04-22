using Horeca.Core.Handlers.Commands.Units;
using Horeca.Core.Handlers.Queries.Units;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Units;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Horeca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IMediator mediator;

        public UnitController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///  Get list of Units by restaurant id
        /// </summary>
        /// <param name="restaurantId">restuarant id </param>
        /// <returns></returns>
        /// <response code="200">Success retrieving Unit list</response>
        /// <response code="400">Bad request</response>
        [PermissionAuthorize(nameof(Shared.Data.Entities.Unit), Permissions.Create)]
        [HttpGet]
        [Route("Restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(IEnumerable<UnitDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get([FromRoute] int restaurantId)
        {
            return Ok(await mediator.Send(new GetAllUnitsQuery(restaurantId)));
        }

        /// <summary>
        /// Create new Unit
        /// </summary>
        /// <param name="model"></param>
        /// <param name="restaurantId">restaurant id </param>
        /// <returns></returns>
        /// <response code="201">Success creating new Unit</response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [Route("/Restaurant/{restaurantId}")]
        [PermissionAuthorize(nameof(Shared.Data.Entities.Unit), Permissions.Create)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateUnitDto model, [FromRoute] int restaurantId)
        {
            model.RestaurantId = restaurantId;
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new CreateUnitCommand(model, restaurantId)));
        }

        /// <summary>
        /// Retrieve Unit by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        /// <response code="200">Success Retrieve Unit by Id</response>
        /// <response code="400">Bad request</response
        [HttpGet]
        [Route("{id}/Restaurant/{restaurantId}")]
        [PermissionAuthorize(nameof(Shared.Data.Entities.Unit), Permissions.Read)]
        [ProducesResponseType(typeof(UnitDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById([FromRoute] int id, [FromRoute] int restaurantId)
        {
            return Ok(await mediator.Send(new GetUnitByIdQuery(id, restaurantId)));
        }

        /// <summary>
        ///  Delete an existing Unit
        /// </summary>
        /// <param name="id"></param>
        ///
        /// <returns></returns>
        /// <response code="204">Success delete an existing Unit</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Shared.Data.Entities.Unit), Permissions.Delete)]
        [HttpDelete]
        [Route("{id}/Restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteUnitCommand(id)));
        }

        /// <summary>
        /// Update an existing Unit
        /// </summary>
        /// <param name="model"></param>
        /// <param name="restaurantId"></param>
        ///
        /// <returns></returns>
        /// <response code="200">Success updating existing Unit</response>
        /// <response code="400">Bad request</response>
        [HttpPut]
        [Route("{id}/Restaurant/{restaurantId}")]
        [PermissionAuthorize(nameof(Shared.Data.Entities.Unit), Permissions.Update)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateUnitDto model, [FromRoute] int restaurantId, [FromRoute] int id)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new EditUnitCommand(model, id, restaurantId)));
        }
    }
}