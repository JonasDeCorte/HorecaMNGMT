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
        [Route("Restaurant/{restaurantId}")]
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
        [Route("Restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateMenuDto model, [FromRoute] int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new CreateMenuCommand(model, restaurantId)));
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
        [Route("{id}/Restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(MenuDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById([FromRoute] int id, [FromRoute] int restaurantId)
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
        [Route("{id}/Restaurant/{restaurantId}")]
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
        [Route("{id}/Restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateMenuDto model, [FromRoute] int id, [FromRoute] int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new EditMenuCommand(model, id, restaurantId)));
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
        [Route("{id}/dishes/Restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> AddDishToMenu([FromRoute] int id, [FromBody] MutateDishMenuDto model, [FromRoute] int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddDishMenuCommand(model, id, restaurantId)));
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
        [Route("{id}/dishes/Restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(MenuDishesByIdDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetDishesByMenuId([FromRoute] int id, [FromRoute] int restaurantId)
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
        [Route("{id}/dishes/{dishId}/Restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteDishByMenuId([FromBody] DeleteDishMenuDto model, [FromRoute] int id, [FromRoute] int dishId, [FromRoute] int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteDishMenuCommand(model, id, dishId, restaurantId)));
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
        [Route("{id}/dishes/{dishId}/Restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> EditDishFromMenu([FromRoute] int id, [FromRoute] int dishId, [FromBody] MutateDishMenuDto model, [FromRoute] int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new EditDishMenuCommand(model, id, dishId, restaurantId)));
        }
    }
}