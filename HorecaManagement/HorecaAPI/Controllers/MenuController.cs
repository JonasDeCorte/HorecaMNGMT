using Horeca.Core.Handlers.Commands.Menus;
using Horeca.Core.Handlers.Queries.Menus;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Horeca.API.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMediator mediator;

        public MenuController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///  Get list of Menus
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving Menu list</response>
        /// <response code="400">Bad request</response>
        [PermissionAuthorize(nameof(Menu), Permissions.Read)]
        [HttpGet]
        [Route("restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(IEnumerable<MenuDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get([FromRoute] int restaurantId)
        {
            return Ok(await mediator.Send(new GetAllMenusQuery(restaurantId)));
        }

        /// <summary>
        /// Create new Menu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success creating new Menu</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(Menu), Permissions.Create)]
        [HttpPost]
        [Route("restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateMenuDto model, [FromRoute] int restaurantId)
        {
            model.RestaurantId = restaurantId;
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new CreateMenuCommand(model)));
        }

        /// <summary>
        /// Retrieve Menu by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success Retrieve Menu by Id</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(Menu), Permissions.Read)]
        [HttpGet]
        [Route("{id}/restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(MenuDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id, [FromRoute] int restaurantId)
        {
            return Ok(await mediator.Send(new GetMenuByIdQuery(id, restaurantId)));
        }

        /// <summary>
        ///  Delete an existing Menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an existing Menu</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(Menu), Permissions.Delete)]
        [HttpDelete]
        [Route("{id}/restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteMenuCommand(id)));
        }

        /// <summary>
        /// Update an existing Menu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Success updating existing Menu</response>
        /// <response code="400">Bad request</response>
        ///
        [PermissionAuthorize(nameof(Menu), Permissions.Update)]
        [HttpPut]
        [Route("{id}/restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateMenuDto model, [FromRoute] int id, [FromRoute] int restaurantId)
        {
            model.Id = id;
            model.RestaurantId = restaurantId;
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new EditMenuCommand(model)));
        }

        /// <summary>
        /// add a dish to an existing menu
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Success adding a new dish to an menu Dish</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(Menu), Permissions.Create)]
        [HttpPost]
        [Route("{id}/dishes/restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> AddDishToMenu([FromRoute] int id, [FromBody] MutateDishMenuDto model, [FromRoute] int restaurantId)
        {
            model.Id = id;
            model.RestaurantId = restaurantId;
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddDishMenuCommand(model)));
        }

        /// <summary>
        /// Retrieve List of dishes from menu by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success List of dishes from menu by Id</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(Menu), Permissions.Read)]
        [HttpGet]
        [Route("{id}/dishes/restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(MenuDishesByIdDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetDishesByMenuhId([FromRoute] int id, [FromRoute] int restaurantId)
        {
            return Ok(await mediator.Send(new GetDishesByMenuIdQuery(id, restaurantId)));
        }

        /// <summary>
        ///  Delete an existing dish from a menu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dishId"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an existing Dish from a menu </response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(Menu), Permissions.Delete)]
        [HttpDelete]
        [Route("{id}/dishes/{dishId}/restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteDishByMenuId([FromRoute] int id, [FromRoute] int dishId, [FromRoute] int restaurantId)
        {
            var model = new DeleteDishMenuDto { DishId = dishId, MenuId = id, RestaurantId = restaurantId };
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteDishMenuCommand(model)));
        }

        /// <summary>
        /// edit an existing dish from an existing menu
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dishId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Success editing an existing dish from an existing menu</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(Menu), Permissions.Update)]
        [HttpPut]
        [Route("{id}/dishes/{dishId}/restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> EditDishFromMenu([FromRoute] int id, [FromRoute] int dishId, [FromBody] MutateDishMenuDto model, [FromRoute] int restaurantId)
        {
            model.Id = id;
            model.Dish.Id = dishId;
            model.RestaurantId = restaurantId;
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new EditDishMenuCommand(model)));
        }
    }
}