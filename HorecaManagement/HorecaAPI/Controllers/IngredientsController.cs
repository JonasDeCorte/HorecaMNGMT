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
    public class IngredientsController : ControllerBase
    {
        private readonly IMediator mediator;

        public IngredientsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///  Get list of Ingredients
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving ingredient list</response>
        /// <response code="400">Bad request</response>
        ///
        [PermissionAuthorize(nameof(Ingredient), Permissions.Read)]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IngredientDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetAllIngredientsQuery()));
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
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateIngredientDto model)
        {
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
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateIngredientDto model)
        {
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
        [Route("{id}")]
        [ProducesResponseType(typeof(IngredientDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await mediator.Send(new GetIngredientByIdQuery(id)));
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
        [Route("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteIngredientCommand(id)));
        }
    }
}