using Horeca.Core.Handlers.Commands.Accounts;
using Horeca.Core.Handlers.Queries.Accounts;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Horeca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)

        {
            this.mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto model)
        {
            var command = new LoginCommand(model);
            var response = await mediator.Send(command);
            if (response is not null)
            {
                return Ok(response);
            }
            return Unauthorized();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            var command = new RegisterCommand(model);
            var response = await mediator.Send(command);

            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserDto model)
        {
            var command = new RegisterAdminCommand(model);
            var response = await mediator.Send(command);

            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpPost]
        [Route("User/{username}/Roles")]
        public async Task<IActionResult> AddRolesToUser([FromRoute] string username, [FromBody] MutateRolesUserDto model)
        {
            model.Username = username;
            var command = new AddRolesToUserCommand(model);
            var response = await mediator.Send(command);

            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpDelete]
        [Route("User/{username}/Roles")]
        public async Task<IActionResult> DeleteRolesFromUser([FromRoute] string username, [FromBody] MutateRolesUserDto model)
        {
            model.Username = username;
            var command = new DeleteRolesFromUserCommand(model);
            var response = await mediator.Send(command);

            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpGet]
        [Route("User/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var command = new GetUserByUsernameQuery(username);
            var response = await mediator.Send(command);

            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpGet]
        [Route("User")]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetAllUsersQuery();
            var response = await mediator.Send(command);

            return StatusCode((int)HttpStatusCode.Created, response);
        }
    }
}