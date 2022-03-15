using Horeca.Core.Handlers.Commands.Reservations;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Reservation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HorecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReservationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Create a new reservation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success adding a new reservation </response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateReservationDto model)
        {
            var command = new AddReservationCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }
    }
}