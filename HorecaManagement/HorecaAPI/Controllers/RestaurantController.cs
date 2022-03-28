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

            return Ok(mediator.Send(new GetRestaurantByIdQuery(id)));
        }
    }
}