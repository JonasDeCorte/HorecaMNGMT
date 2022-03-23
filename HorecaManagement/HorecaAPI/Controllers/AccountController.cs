using Horeca.Core.Handlers.Commands.Accounts;
using Horeca.Core.Handlers.Commands.UserPermissions;
using Horeca.Core.Handlers.Queries.Accounts;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.UserPermissions;
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

        [HttpGet("me")]
        public IActionResult Get()
        {
            // return all the user claims in all identities
            return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
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

        /// <summary>
        /// adds permissions to the user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UserPermissions")]
        public async Task<IActionResult> ManageUserPermissions([FromBody] MutateUserPermissionsDto model)
        {
            var command = new AddUserPermissionsCommand(model);
            var response = await mediator.Send(command);

            return StatusCode((int)HttpStatusCode.OK, response);
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