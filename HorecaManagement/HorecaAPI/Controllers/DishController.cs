using Horeca.Core.Handlers.Commands.Dishes;
using Horeca.Core.Handlers.Queries.Dishes;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Constants;
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
        [Route(RouteConstants.DishConstants.Get)]
        [ProducesResponseType(typeof(IEnumerable<DishDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get(int restaurantId)
        {
            return Ok(await mediator.Send(new GetAllDishesQuery(restaurantId)));
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
        [Route(RouteConstants.DishConstants.Post)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateDishDto model, int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new CreateDishCommand(model, restaurantId)));
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
        [Route(RouteConstants.DishConstants.GetDishById)]
        [ProducesResponseType(typeof(DishDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id, int restaurantId)
        {
            return Ok(await mediator.Send(new GetDishByIdQuery(id, restaurantId)));
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
        [Route(RouteConstants.DishConstants.Delete)]
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
        [Route(RouteConstants.DishConstants.Update)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateDishDto model, int id, int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new EditDishCommand(model, id, restaurantId)));
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
        [Route(RouteConstants.DishConstants.GetIngredientsByDishId)]
        [ProducesResponseType(typeof(DishIngredientsByIdDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetIngredientsByDishId(int id, int restaurantId)
        {
            return Ok(await mediator.Send(new GetIngredientsByDishIdQuery(id, restaurantId)));
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
        [Route(RouteConstants.DishConstants.AddIngredientToDish)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> AddIngredientToDish(int id, [FromBody] MutateIngredientByDishDto model, int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddIngredientDishCommand(model, id, restaurantId)));
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
        [Route(RouteConstants.DishConstants.EditIngredientFromDish)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> EditIngredientFromDish(int id, int ingredientId, [FromBody] MutateIngredientByDishDto model, int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new EditIngredientDishCommand(model, id, ingredientId, restaurantId)));
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
        [Route(RouteConstants.DishConstants.DeleteById)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById([FromBody] DeleteIngredientDishDto model, int id, int ingredientId, int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteIngredientDishCommand(model, id, ingredientId, restaurantId)));
        }
    }
}