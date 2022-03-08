using Horeca.Core.Handlers.Commands.Dishes;
using Horeca.Core.Handlers.Queries.Dishes;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Dishes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HorecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///  Get list of Dishes
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving Dish list</response>
        /// <response code="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DishDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllDishesQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Create a new Dish
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success creating new Dish</response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateDishDto model)
        {
            var command = new CreateDishCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        /// Retrieve Dish by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success Retrieving Dish by Id</response>
        /// <response code="400">Bad request</response
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(DishDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetDishByIdQuery(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        ///  Delete an existing Dish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an exsiting Dish</response>
        /// <response code="400">Bad request</response
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            var command = new DeleteDishCommand(id);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Update an exsiting Dish
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Success updating exsiting Dish</response>
        /// <response code="400">Bad request</response>
        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateDishDto model)
        {
            var command = new EditDishCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Retrieve List of ingredients from dish by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success List of ingredients from dish  by Id</response>
        /// <response code="400">Bad request</response
        [HttpGet]
        [Route("{id}/ingredients")]
        [ProducesResponseType(typeof(DishIngredientsByIdDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetIngredientsByDishId(int id)
        {
            var query = new GetIngredientsByDishIdQuery(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// add an ingredient to an existing Dish
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success adding a new ingredient to an existing Dish</response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [Route("{id}/ingredients")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> AddIngredientToDish([FromBody] MutateIngredientByDishDto model)
        {
            var command = new AddIngredientDishCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        /// edit an existing ingredient from an existing Dish
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success editing an existing ingredient from  an existing Dish</response>
        /// <response code="400">Bad request</response
        [HttpPut]
        [Route("{id}/ingredients/{ingredientsId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> EditIngredientFromDish([FromBody] MutateIngredientByDishDto model)
        {
            var command = new EditIngredientDishCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        ///  Delete an existing ingredient from a dish
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an exsiting ingredient from a Dish</response>
        /// <response code="400">Bad request</response
        [HttpDelete]
        [Route("{id}/ingredients/{ingredientsId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(DeleteIngredientDishDto model)
        {
            var command = new DeleteIngredientDishCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
        }
    }
}