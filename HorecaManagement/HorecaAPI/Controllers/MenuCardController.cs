﻿using Horeca.Core.Handlers.Commands.MenuCards;
using Horeca.Core.Handlers.Queries.MenuCards;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
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
        [PermissionAuthorize(nameof(MenuCard), Permissions.Read)]
        ///
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MenuCardDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllMenuCardsQuery();
            var response = await mediator.Send(query);
            return Ok(response);
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
        [Route("{id}")]
        [ProducesResponseType(typeof(MenuCardDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetMenuCardByIdQuery(id);
            var response = await mediator.Send(query);
            return Ok(response);
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
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateMenuCardDto model)
        {
            var command = new CreateMenuCardCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
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
        [Route("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            var command = new DeleteMenuCardCommand(id);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
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
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateMenuCardDto model)
        {
            var command = new EditMenuCardCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
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
        [Route("{id}/menus")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> AddMenuToMenuCard([FromRoute] int id, [FromBody] MutateMenuMenuCardDto model)
        {
            model.MenuCardId = id;
            var command = new AddMenuMenuCardCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
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
        [Route("{id}/dishes")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> AddDishToMenuCard([FromRoute] int id, [FromBody] MutateDishMenuCardDto model)
        {
            model.MenuCardId = id;
            var command = new AddDishMenuCardCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
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
        [Route("{id}/menus/dishes")]
        [ProducesResponseType(typeof(MenuCardsByIdDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetFullMenuCard(int id)
        {
            var query = new GetMenuCardWithAllDependenciesByIdQuery(id);
            var response = await mediator.Send(query);
            return Ok(response);
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
        [Route("{id}/dishes/{dishId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> EditDishFromMenuCard([FromRoute] int id, [FromRoute] int dishId, [FromBody] MutateDishMenuCardDto model)
        {
            model.MenuCardId = id;
            model.Dish.Id = dishId;
            var command = new EditDishMenuCardCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
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
        [Route("{id}/menus/{menuId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> EditMenuFromMenuCard([FromRoute] int id, [FromRoute] int menuId, [FromBody] MutateMenuMenuCardDto model)
        {
            model.MenuCardId = id;
            model.Menu.Id = menuId;
            var command = new EditMenuMenuCardCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
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
        [Route("{id}/menus/{menuId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteMenuMenuCardById([FromRoute] int id, [FromRoute] int menuId)
        {
            var command = new DeleteMenuMenuCardCommand(new DeleteMenuMenuCardDto { MenuCardId = id, MenuId = menuId });
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
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
        [Route("{id}/dishes/{dishId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteDishMenuCardById([FromRoute] int id, [FromRoute] int dishId)
        {
            var command = new DeleteDishMenuCardCommand(new DeleteDishMenuCardDto { MenuCardId = id, DishId = dishId });
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
        }
    }
}