using Horeca.Core.Handlers.Commands.Ingredients;
using Horeca.Core.Handlers.Queries.Ingredients;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Ingredients;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Horeca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IMediator mediator;

        public IngredientController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///  Get list of Ingredients by restaurantId
        /// </summary>
        /// <param name="restaurantId">restaurant id </param>
        /// <returns></returns>
        /// <response code="200">Success retrieving ingredient list by restaurant id </response>
        /// <response code="400">Bad request</response>
        ///
        [PermissionAuthorize(nameof(Ingredient), Permissions.Read)]
        [HttpGet]
        [Route("Restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(IEnumerable<IngredientDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetAllIngredientsByRestaurantId([FromRoute] int restaurantId)
        {
            return Ok(await mediator.Send(new GetAllIngredientsByRestaurantIdQuery(restaurantId)));
        }

        /// <summary>
        /// Create new ingredient
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success creating new ingredient</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(Ingredient), Permissions.Create)]
        [HttpPost]
        [Route("Restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateIngredientDto model, [FromRoute] int restaurantId)
        {
            model.RestaurantId = restaurantId;
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new CreateIngredientCommand(model)));
        }

        /// <summary>
        /// Update an existing ingredient
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Success updating existing ingredient</response>
        /// <response code="400">Bad request</response>
        ///
        [PermissionAuthorize(nameof(Ingredient), Permissions.Update)]
        [HttpPut]
        [Route("{id}/Restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MutateIngredientDto model, [FromRoute] int restaurantId)
        {
            model.RestaurantId = restaurantId;
            model.Id = id;
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new EditIngredientCommand(model)));
        }

        /// <summary>
        /// Retrieve ingredient by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success Retrieve ingredient by Id</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(Ingredient), Permissions.Read)]
        [HttpGet]
        [Route("{id}/Restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(IngredientDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById([FromRoute] int id, [FromRoute] int restaurantId)
        {
            return Ok(await mediator.Send(new GetIngredientByIdQuery(id, restaurantId)));
        }

        /// <summary>
        ///  Delete an existing ingredient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an existing ingredient</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(Ingredient), Permissions.Delete)]
        [HttpDelete]
        [Route("{id}/Restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteIngredientCommand(id)));
        }
    }
}