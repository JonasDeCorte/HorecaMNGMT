using Horeca.Core.Handlers.Commands.Restaurants;
using Horeca.Core.Handlers.Queries.Restaurants;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Restaurants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Horeca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IMediator mediator;

        public RestaurantController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///  Get list of Restaurants
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving restaurants list</response>
        /// <response code="400">Bad request</response>
        [PermissionAuthorize(nameof(Restaurant), Permissions.Read)]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RestaurantDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetAllRestaurantsQuery()));
        }

        /// <summary>
        /// Retrieve restaurant by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success Retrieving restaurant by Id</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Restaurant), Permissions.Read)]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(DetailRestaurantDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await mediator.Send(new GetRestaurantByIdQuery(id)));
        }

        /// <summary>
        /// Create a new restaurant
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success creating a new restaurant</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Restaurant), Permissions.Create)]
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateRestaurantDto model)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddRestaurantCommand(model)));
        }

        /// <summary>
        ///  Delete an existing Restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an existing Restaurant</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Restaurant), Permissions.Delete)]
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteRestaurantCommand(id)));
        }
    }
}