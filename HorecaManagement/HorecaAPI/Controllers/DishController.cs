using Horeca.Core.Handlers.Commands.Dishes;
using Horeca.Core.Handlers.Queries.Dishes;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Dishes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Horeca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IMediator mediator;

        public DishController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///  Get list of Dishes
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving Dish list</response>
        /// <response code="400">Bad request</response>
        [PermissionAuthorize(nameof(Dish), Permissions.Read)]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DishDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetAllDishesQuery()));
        }

        /// <summary>
        /// Create a new Dish
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success creating new Dish</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Dish), Permissions.Create)]
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateDishDto model)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new CreateDishCommand(model)));
        }

        /// <summary>
        /// Retrieve Dish by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success Retrieving Dish by Id</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Dish), Permissions.Read)]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(DishDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await mediator.Send(new GetDishByIdQuery(id)));
        }

        /// <summary>
        ///  Delete an existing Dish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an existing Dish</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Dish), Permissions.Delete)]
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteDishCommand(id)));
        }

        /// <summary>
        /// Update an existing Dish
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Success updating existing Dish</response>
        /// <response code="400">Bad request</response>
        [PermissionAuthorize(nameof(Dish), Permissions.Update)]
        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateDishDto model)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new EditDishCommand(model)));
        }

        /// <summary>
        /// Retrieve List of ingredients from dish by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success List of ingredients from dish  by Id</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Dish), Permissions.Read)]
        [HttpGet]
        [Route("{id}/ingredients")]
        [ProducesResponseType(typeof(DishIngredientsByIdDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetIngredientsByDishId(int id)
        {
            return Ok(await mediator.Send(new GetIngredientsByDishIdQuery(id)));
        }

        /// <summary>
        /// add an ingredient to an existing Dish
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Success adding a new ingredient to an existing Dish</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Dish), Permissions.Create)]
        [HttpPost]
        [Route("{id}/ingredients")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> AddIngredientToDish([FromRoute] int id, [FromBody] MutateIngredientByDishDto model)
        {
            model.Id = id;
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddIngredientDishCommand(model)));
        }

        /// <summary>
        /// edit an existing ingredient from an existing Dish
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <param name="ingredientId"></param>
        /// <returns></returns>
        /// <response code="201">Success editing an existing ingredient from  an existing Dish</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Dish), Permissions.Update)]
        [HttpPut]
        [Route("{id}/ingredients/{ingredientId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> EditIngredientFromDish([FromRoute] int id, [FromRoute] int ingredientId, [FromBody] MutateIngredientByDishDto model)
        {
            model.Id = id;
            model.Ingredient.Id = ingredientId;

            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new EditIngredientDishCommand(model)));
        }

        /// <summary>
        ///  Delete an existing ingredient from a dish
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ingredientId"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an existing ingredient from a Dish</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Dish), Permissions.Delete)]
        [HttpDelete]
        [Route("{id}/ingredients/{ingredientId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById([FromRoute] int id, [FromRoute] int ingredientId)
        {
            var model = new DeleteIngredientDishDto
            {
                DishId = id,
                IngredientId = ingredientId
            };

            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteIngredientDishCommand(model)));
        }
    }
}