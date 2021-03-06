using Horeca.Core.Handlers.Commands.Accounts;
using Horeca.Core.Handlers.Commands.UserPermissions;
using Horeca.Core.Handlers.Queries.Accounts;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.Tokens;
using Horeca.Shared.Dtos.UserPermissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
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

        /// <summary>
        /// return access token superadmin
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route(RouteConstants.AccountConstants.LoginSuperAdmin)]
        public async Task<IActionResult> LoginSuperAdmin()
        {
            return Ok(await mediator.Send(new LoginCommand(new LoginUserDto() { Password = "SuperAdmin123!", Username = "SuperAdmin" })));
        }

        [HttpGet(RouteConstants.AccountConstants.GetUserClaims)]
        [AllowAnonymous]
        public IActionResult GetUserClaims()
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
        [Route(RouteConstants.AccountConstants.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto model)
        {
            return Ok(await mediator.Send(new RefreshCommand(model)));
        }

        /// <summary>
        /// call to remove the refresh token
        /// </summary>
        /// <param name="token">refresh token </param>
        /// <returns></returns>
        [HttpDelete]
        [AllowAnonymous]
        [Route(RouteConstants.AccountConstants.RevokeToken)]
        public async Task<IActionResult> RevokeToken([FromBody] string token)
        {
            return Ok(await mediator.Send(new RevokeTokenCommand(token)));
        }

        /// <summary>
        /// authenticate the user and return access token + refresh token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route(RouteConstants.AccountConstants.Login)]
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
        [Route(RouteConstants.AccountConstants.Register)]
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
        [Route(RouteConstants.AccountConstants.RegisterAdmin)]
        [PermissionAuthorize(nameof(ApplicationUser), Permissions.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
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
        [Route(RouteConstants.AccountConstants.UserPermissions)]
        [PermissionAuthorize(nameof(Permission), Permissions.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
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
        [Route(RouteConstants.AccountConstants.UserPermissions)]
        [PermissionAuthorize(nameof(ApplicationUser), Permissions.Read)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new GetUserByUsernameQuery(username)));
        }

        /// <summary>
        ///  Delete an existing User
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <response code="204">Successfully deleted an existing User</response>
        /// <response code="400">Bad request</response
        [PermissionAuthorize(nameof(ApplicationUser), Permissions.Delete)]
        [HttpDelete]
        [Route(RouteConstants.AccountConstants.DeleteUser)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteByUsername(string username)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteUserCommand(username)));
        }

        /// <summary>
        /// gets all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(RouteConstants.AccountConstants.GetAllUsers)]
        [PermissionAuthorize(nameof(ApplicationUser), Permissions.Read)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
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
        [Route(RouteConstants.AccountConstants.UserPermissions)]
        [PermissionAuthorize(nameof(Permission), Permissions.Delete)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> RemovePermissions([FromBody] DeleteUserPermissionsDto model)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteUserPermissionsCommand(model)));
        }
    }
}