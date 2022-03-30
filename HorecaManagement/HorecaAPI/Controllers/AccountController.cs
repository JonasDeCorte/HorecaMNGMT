﻿using Horeca.Core.Handlers.Commands.Accounts;
using Horeca.Core.Handlers.Commands.UserPermissions;
using Horeca.Core.Handlers.Queries.Accounts;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.Tokens;
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
        [AllowAnonymous]
        public IActionResult Get()
        {
            // return all the user claims in all identities
            return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
        }

        /// <summary>
        /// call to update the access token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto model)
        {
            return Ok(await mediator.Send(new RefreshCommand(model)));
        }

        /// <summary>
        /// logs in call
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto model)
        {
            return Ok(await mediator.Send(new LoginCommand(model)));
        }

        /// <summary>
        /// register a normal user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new RegisterCommand(model)));
        }

        /// <summary>
        /// registers an admin (has all permissions)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register-admin")]
        [PermissionAuthorize(nameof(ApplicationUser), Permissions.Create)]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserDto model)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new RegisterAdminCommand(model)));
        }

        /// <summary>
        /// adds permissions to the user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [PermissionAuthorize(nameof(Permission), Permissions.Update)]
        [Route("UserPermissions")]
        public async Task<IActionResult> ManageUserPermissions([FromBody] MutateUserPermissionsDto model)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new AddUserPermissionsCommand(model)));
        }

        /// <summary>
        /// Gets user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/{username}")]
        [PermissionAuthorize(nameof(ApplicationUser), Permissions.Read)]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new GetUserByUsernameQuery(username)));
        }

        /// <summary>
        /// gets all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("User")]
        [PermissionAuthorize(nameof(ApplicationUser), Permissions.Read)]
        public async Task<IActionResult> GetAll()
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new GetAllUsersQuery()));
        }

        /// <summary>
        /// deletes a specified users permissions
        /// </summary>
        /// <param name="username"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("UserPermissions/{username}")]
        [PermissionAuthorize(nameof(Permission), Permissions.Delete)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> RemovePermissions([FromRoute] string username, DeleteUserPermissionsDto model)
        {
            model.UserName = username;
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteUserPermissionsCommand(model)));
        }
    }
}