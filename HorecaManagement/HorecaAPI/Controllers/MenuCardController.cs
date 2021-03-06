using Horeca.Core.Handlers.Commands.MenuCards;
using Horeca.Core.Handlers.Queries.MenuCards;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Horeca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuCardController : ControllerBase
    {
        private readonly IMediator mediator;

        public MenuCardController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///  Get list of MenuCards
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving MenuCard list</response>
        /// <response code="400">Bad request</response>
        ///
        [PermissionAuthorize(nameof(MenuCard), Permissions.Read)]
        [HttpGet]
        [Route(RouteConstants.MenuCardConstants.GetAllMenuCardsByRestaurantId)]
        [ProducesResponseType(typeof(IEnumerable<MenuCardDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get(int restaurantId)
        {
            return Ok(await mediator.Send(new GetAllMenuCardsQuery(restaurantId)));
        }

        /// <summary>
        /// Retrieve MenuCard by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success Retrieving MenuCard by Id</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(MenuCard), Permissions.Read)]
        [HttpGet]
        [Route(RouteConstants.MenuCardConstants.GetById)]
        [ProducesResponseType(typeof(MenuCardDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id, int restaurantId)
        {
            return Ok(await mediator.Send(new GetMenuCardByIdQuery(id, restaurantId)));
        }

        /// <summary>
        /// Create new MenuCard
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success creating new MenuCard </response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(MenuCard), Permissions.Create)]
        [HttpPost]
        [Route(RouteConstants.MenuCardConstants.Post)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateMenuCardDto model, int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new CreateMenuCardCommand(model, restaurantId)));
        }

        /// <summary>
        ///  Delete an existing MenuCard
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an existing MenuCard</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(MenuCard), Permissions.Delete)]
        [HttpDelete]
        [Route(RouteConstants.MenuCardConstants.Delete)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteMenuCardCommand(id)));
        }

        /// <summary>
        /// Update an existing MenuCard
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Success updating existing MenuCard</response>
        /// <response code="400">Bad request</response>
        [PermissionAuthorize(nameof(MenuCard), Permissions.Update)]
        [HttpPut]
        [Route(RouteConstants.MenuCardConstants.Update)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateMenuCardDto model, int id, int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new EditMenuCardCommand(model, id, restaurantId)));
        }

        /// <summary>
        /// add a menu to an existing menuCard
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Success adding a new menu to a menuCard</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(MenuCard), Permissions.Create)]
        [HttpPost]
        [Route(RouteConstants.MenuCardConstants.PostMenu)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> AddMenuToMenuCard(int id, [FromBody] MutateMenuMenuCardDto model, int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddMenuMenuCardCommand(model, id, restaurantId)));
        }

        /// <summary>
        /// add a Dish to an existing menuCard
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Success adding a new Dish to a menuCard</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(MenuCard), Permissions.Create)]
        [HttpPost]
        [Route(RouteConstants.MenuCardConstants.PostDishes)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> AddDishToMenuCard(int id, [FromBody] MutateDishMenuCardDto model, int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddDishMenuCardCommand(model, id, restaurantId)));
        }

        /// <summary>
        /// Retrieve the full menu card in detail.
        /// </summary>
        /// <param name="id"></param
        /// <returns></returns>
        /// <response code="200">Success List of menus and dishes from menu by Id</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(MenuCard), Permissions.Read)]
        [HttpGet]
        [Route(RouteConstants.MenuCardConstants.GetFullMenuCard)]
        [ProducesResponseType(typeof(MenuCardsByIdDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetFullMenuCard(int id, int restaurantId)
        {
            return Ok(await mediator.Send(new GetMenuCardWithAllDependenciesByIdQuery(id, restaurantId)));
        }

        /// <summary>
        /// edit an existing dish from an existing menuCard
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <param name="dishId"></param>
        /// <returns></returns>
        /// <response code="201">Success editing an existing dish from an existing menuCard</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(MenuCard), Permissions.Update)]
        [HttpPut]
        [Route(RouteConstants.MenuCardConstants.UpdateDish)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> EditDishFromMenuCard(int id, int dishId, [FromBody] MutateDishMenuCardDto model, int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new EditDishMenuCardCommand(model, id, dishId, restaurantId)));
        }

        /// <summary>
        /// edit an existing menu from an existing menuCard
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        /// <response code="201">Success editing an existing menu from an existing menuCard</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(MenuCard), Permissions.Update)]
        [HttpPut]
        [Route(RouteConstants.MenuCardConstants.UpdateMenu)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> EditMenuFromMenuCard(int id, int menuId, [FromBody] MutateMenuMenuCardDto model, int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new EditMenuMenuCardCommand(model, id, menuId, restaurantId)));
        }

        /// <summary>
        ///  Delete an existing menu from a menuCard
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an existing  menu from a menuCard</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(MenuCard), Permissions.Delete)]
        [HttpDelete]
        [Route(RouteConstants.MenuCardConstants.DeleteMenu)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteMenuMenuCardById([FromBody] DeleteMenuMenuCardDto model, int id, int menuId, int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteMenuMenuCardCommand(model, id, menuId, restaurantId)));
        }

        /// <summary>
        ///  Delete an existing dish from a menuCard
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dishId"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an existing  dish from a menuCard</response>
        /// <response code="400">Bad request</response
        ///
        [PermissionAuthorize(nameof(MenuCard), Permissions.Delete)]
        [HttpDelete]
        [Route(RouteConstants.MenuCardConstants.DeleteDish)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteDishMenuCardById([FromBody] DeleteDishMenuCardDto model, int id, int dishId, int restaurantId)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteDishMenuCardCommand(model, id, dishId, restaurantId)));
        }
    }
}