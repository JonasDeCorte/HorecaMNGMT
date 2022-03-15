﻿using Horeca.Core.Handlers.Commands.Ingredients;
using Horeca.Core.Handlers.Queries.Ingredients;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Ingredients;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Net;

namespace Horeca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IngredientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///  Get list of Ingredients
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving ingredient list</response>
        /// <response code="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IngredientDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get()
        {
            logger.Info("requesting all ingredients");
            var query = new GetAllIngredientsQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Create new ingredient
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success creating new ingredient</response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateIngredientDto model)
        {
            logger.Info("requesting to create an ingredient");

            var command = new CreateIngredientCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        /// Update an exsiting ingredient
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Success updating exsiting ingredient</response>
        /// <response code="400">Bad request</response>
        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateIngredientDto model)
        {
            logger.Info("requesting to update an ingredient");

            var command = new EditIngredientCommand(model);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Retrieve ingredient by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success Retrieve ingredient by Id</response>
        /// <response code="400">Bad request</response
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(IngredientDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id)
        {
            logger.Info("requesting to get an ingredient");

            var query = new GetIngredientByIdQuery(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        ///  Delete an existing ingredient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an exsiting ingredient</response>
        /// <response code="400">Bad request</response
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            logger.Info("requesting to delete an ingredient");

            var command = new DeleteIngredientCommand(id);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
        }
    }
}