using Horeca.Core.Handlers.Queries.Permissions;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Horeca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator mediator;

        public PermissionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        ///  Get list of permissions
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success retrieving permissions list</response>
        /// <response code="400">Bad request</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PermissionDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllPermissionsQuery();

            var response = await mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Retrieve permission by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success Retrieve permission by Id</response>
        /// <response code="400">Bad request</response
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(PermissionDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetPermissionByIdQuery(id);
            var response = await mediator.Send(query);
            return Ok(response);
        }
    }
}