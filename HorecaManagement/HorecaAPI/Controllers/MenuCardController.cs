﻿using Horeca.Core.Handlers.Commands.MenuCards;
using Horeca.Core.Handlers.Queries.MenuCards;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HorecaAPI.Controllers
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
        /// <response code="204">Success delete an exsiting MenuCard</response>
        /// <response code="400">Bad request</response
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
        /// Update an exsiting MenuCard
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Success updating exsiting MenuCard</response>
        /// <response code="400">Bad request</response>
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
        /// <returns></returns>
        /// <response code="201">Success adding a new menu to a menuCard</response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [Route("{id}/menus")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> AddMenuToMenuCard([FromBody] MutateMenuMenuCardDto model)
        {
            var command = new AddMenuMenuCardCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        /// add a menu to an existing menuCard
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success adding a new menu to a menuCard</response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [Route("{id}/dishes")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> AddDishToMenuCard([FromBody] MutateDishMenuCardDto model)
        {
            var command = new AddDishMenuCardCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        /// Retrieve the full menu card in detail.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success List of menus and dishes from menu by Id</response>
        /// <response code="400">Bad request</response
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
        /// <returns></returns>
        /// <response code="201">Success editing an existing dish from an existing menuCard</response>
        /// <response code="400">Bad request</response
        [HttpPut]
        [Route("{id}/dishes/{dishId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> EditDishFromMenuCard([FromBody] MutateDishMenuCardDto model)
        {
            var command = new EditDishMenuCardCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        /// edit an existing menu from an existing menuCard
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success editing an existing menu from an existing menuCard</response>
        /// <response code="400">Bad request</response
        [HttpPut]
        [Route("{id}/menus/{menuId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> EditMenuFromMenuCard([FromBody] MutateMenuMenuCardDto model)
        {
            var command = new EditMenuMenuCardCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        ///  Delete an existing menu from a menuCard
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an exsiting  menu from a menuCard</response>
        /// <response code="400">Bad request</response
        [HttpDelete]
        [Route("{id}/menus/{menuId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteMenuMenuCardById(DeleteMenuMenuCardDto model)
        {
            var command = new DeleteMenuMenuCardCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        /// <summary>
        ///  Delete an existing dish from a menuCard
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an exsiting  dish from a menuCard</response>
        /// <response code="400">Bad request</response
        [HttpDelete]
        [Route("{id}/dishes/{dishId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteDishMenuCardById(DeleteDishMenuCardDto model)
        {
            var command = new DeleteDishMenuCardCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
        }
    }
}