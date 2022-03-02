using Horeca.Core.Exceptions;
using Horeca.Core.Handlers.Queries.Dishes;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Dishes;
using HorecaCore.Handlers.Commands.Dishes;
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

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DishDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get()
        {
            // create request
            var query = new GetAllDishesQuery();
            // get response
            var response = await _mediator.Send(query);
            // use it
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateDishDto model)
        {
            try
            {
                var command = new CreateDishCommand(model);
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

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(DishDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetDishByIdQuery(id);
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
                var command = new DeleteDishCommand(id);
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

        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] Dish model)
        {
            try
            {
                var command = new EditDishCommand(model);
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
    }
}