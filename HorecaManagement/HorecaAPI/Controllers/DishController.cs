﻿using Horeca.Core.Handlers.Commands.Dishes;
using Horeca.Core.Handlers.Queries.Dishes;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Dishes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Net;

namespace HorecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IMediator _mediator;
        private static Logger logger = LogManager.GetCurrentClassLogger();

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
            logger.Info("requesting all Dishes");

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
            logger.Info("requesting to create a dish");

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
            logger.Info("requesting to get a dish");

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
            logger.Info("requesting to delete a dish");

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
            logger.Info("requesting to update a dish");

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
            logger.Info("requesting a dish by id  with all his ingredients ");

            var query = new GetIngredientsByDishIdQuery(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// add an ingredient to an existing Dish
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Success adding a new ingredient to an existing Dish</response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [Route("{id}/ingredients")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> AddIngredientToDish([FromRoute] int id, [FromBody] MutateIngredientByDishDto model)
        {
            logger.Info("requesting adding an ingredient to a dish");

            model.Id = id;
            var command = new AddIngredientDishCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
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
        [HttpPut]
        [Route("{id}/ingredients/{ingredientId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> EditIngredientFromDish([FromRoute] int id, [FromRoute] int ingredientId, [FromBody] MutateIngredientByDishDto model)
        {
            logger.Info("edit an existing ingredient from an existing Dish");

            model.Id = id;
            model.Ingredient.Id = ingredientId;
            var command = new EditIngredientDishCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        ///  Delete an existing ingredient from a dish
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ingredientId"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an exsiting ingredient from a Dish</response>
        /// <response code="400">Bad request</response
        [HttpDelete]
        [Route("{id}/ingredients/{ingredientId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById([FromRoute] int id, [FromRoute] int ingredientId)
        {
            logger.Info(" Delete an existing ingredient from a dish");

            var model = new DeleteIngredientDishDto
            {
                DishId = id,
                IngredientId = ingredientId
            };
            var command = new DeleteIngredientDishCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
        }
    }
}