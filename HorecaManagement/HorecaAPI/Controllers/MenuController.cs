using Horeca.Core.Handlers.Commands.Menus;
using Horeca.Core.Handlers.Queries.Menus;
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
        private readonly IMediator _mediator;

        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///  Get list of Menus
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving Menu list</response>
        /// <response code="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MenuDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllMenusQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Create new Menu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success creating new Menu</response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateMenuDto model)
        {
            var command = new CreateMenuCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        /// Retrieve Menu by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success Retrieve Menu by Id</response>
        /// <response code="400">Bad request</response
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(MenuDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetMenuByIdQuery(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        ///  Delete an existing Menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an exsiting Menu</response>
        /// <response code="400">Bad request</response
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            var command = new DeleteMenuCommand(id);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Update an exsiting Menu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Success updating exsiting Menu</response>
        /// <response code="400">Bad request</response>
        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateMenuDto model)
        {
            var command = new EditMenuCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// add a dish to an existing menu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success adding a new dish to an menu Dish</response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [Route("{id}/dishes")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> AddDishToMenu([FromBody] MutateDishMenuDto model)
        {
            var command = new AddDishMenuCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        /// Retrieve List of dishes from menu by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success List of dishes from menu by Id</response>
        /// <response code="400">Bad request</response
        [HttpGet]
        [Route("{id}/dishes")]
        [ProducesResponseType(typeof(MenuDishesByIdDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetDishesByMenuhId(int id)
        {
            var query = new GetDishesByMenuIdQuery(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        ///  Delete an existing dish from a menu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an exsiting Dish from a menu </response>
        /// <response code="400">Bad request</response
        [HttpDelete]
        [Route("{id}/dishes/{dishId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteDishByMenuId(DeleteDishMenuDto model)
        {
            var command = new DeleteDishMenuCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// edit an existing dish from an existing menu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success editing an existing dish from an existing menu</response>
        /// <response code="400">Bad request</response
        [HttpPut]
        [Route("{id}/dishes/{dishId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> EditDishFromMenu([FromBody] MutateDishMenuDto model)
        {
            var command = new EditDishMenuCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }
    }
}