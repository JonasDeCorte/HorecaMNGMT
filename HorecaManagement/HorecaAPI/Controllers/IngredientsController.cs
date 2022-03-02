using Horeca.Core.Exceptions;
using Horeca.Core.Handlers.Queries.Ingredients;
using Horeca.Core.Providers.Handlers.Commands;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using HorecaCore.Handlers.Commands;
using HorecaCore.Handlers.Commands.Ingredients;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Horeca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IngredientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Finally in the Controller, we call the Mediator and Send our "QueryRequest"
        /// which is delegated to the QueryRequestHandler and return data from the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IngredientDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get()
        {
            // create request
            var query = new GetAllIngredientsQuery();
            // get response
            var response = await _mediator.Send(query);
            // use it
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateIngredientDto model)
        {
            try
            {
                var command = new CreateIngredientCommand(model);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDto
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] Ingredient model)
        {
            try
            {
                var command = new EditIngredientCommand(model);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDto
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(IngredientDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetIngredientByIdQuery(id);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDto
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                var command = new DeleteIngredientCommand(id);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDto
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }
    }
}