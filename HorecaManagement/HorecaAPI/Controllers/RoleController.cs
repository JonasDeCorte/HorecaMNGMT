using Horeca.Core.Handlers.Commands.Roles;
using Horeca.Core.Handlers.Queries.Roles;
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
    public class RoleController : ControllerBase
    {
        private readonly IMediator mediator;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public RoleController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///  Get list of Roles
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving Roles list</response>
        /// <response code="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoleDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get()
        {
            logger.Info("requesting all roles");

            var query = new GetAllRolesQuery();

            var response = await mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Create new Role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Success creating new Role</response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MutateRoleDto model)
        {
            logger.Info("requesting to create a new role");

            var command = new CreateRoleCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        /// Retrieve role by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success Retrieve role by Id</response>
        /// <response code="400">Bad request</response
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(string id)
        {
            logger.Info("requesting to get a role by id ");

            var query = new GetRoleByIdQuery(id);
            var response = await mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        ///  Delete an existing role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an exsiting role</response>
        /// <response code="400">Bad request</response
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(string id)
        {
            logger.Info("requesting to delete a role");

            var command = new DeleteRoleCommand(id);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Update an exsiting Role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Success updating exsiting Role</response>
        /// <response code="400">Bad request</response>
        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] MutateRoleDto model)
        {
            logger.Info("requesting to update an exsiting role");

            var command = new EditRoleCommand(model);
            var response = await mediator.Send(command);
            return StatusCode((int)HttpStatusCode.OK, response);
        }
    }
}