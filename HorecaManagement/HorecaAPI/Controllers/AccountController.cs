using Horeca.Core.Handlers.Commands.Accounts;
using Horeca.Core.Handlers.Queries.Accounts;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Net;

namespace Horeca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AccountController(IMediator mediator)

        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto model)
        {
            logger.Info("requesting to login");

            var command = new LoginCommand(model);
            var response = await mediator.Send(command);
            if (response is not null)
            {
                return Ok(response);
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            logger.Info("requesting to register");

            var command = new RegisterCommand(model);
            var response = await mediator.Send(command);

            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserDto model)
        {
            logger.Info("requesting to register an admin");

            var command = new RegisterAdminCommand(model);
            var response = await mediator.Send(command);

            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpPost]
        [Route("User/{username}/Roles")]
        public async Task<IActionResult> AddRolesToUser([FromRoute] string username, [FromBody] MutateRolesUserDto model)
        {
            logger.Info("requesting to add roles to a user");

            model.Username = username;
            var command = new AddRolesToUserCommand(model);
            var response = await mediator.Send(command);

            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpDelete]
        [Route("User/{username}/Roles")]
        public async Task<IActionResult> DeleteRolesFromUser([FromRoute] string username, [FromBody] MutateRolesUserDto model)
        {
            logger.Info("requesting to delete roles from a user ");

            model.Username = username;
            var command = new DeleteRolesFromUserCommand(model);
            var response = await mediator.Send(command);

            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpGet]
        [Route("User/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            logger.Info("requesting to get a user by username");

            var command = new GetUserByUsernameQuery(username);
            var response = await mediator.Send(command);

            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpGet]
        [Route("User")]
        public async Task<IActionResult> GetAll()
        {
            logger.Info("requesting to get all users");

            var command = new GetAllUsersQuery();
            var response = await mediator.Send(command);

            return StatusCode((int)HttpStatusCode.Created, response);
        }
    }
}