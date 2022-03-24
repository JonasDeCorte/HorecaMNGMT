using Horeca.Shared.Data.Entities.Account;

namespace Horeca.Shared.Data.Services
{
    /// <summary>
    /// Interface for authentication.
    /// </summary>
    public interface IAuthenticateService
    {
        /// <summary>
        /// Authenticates user.
        /// Takes responsibilities to generate access and refresh token, save refresh token in database
        /// and return instance of <see cref="TokenResultDto"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/>.</param>
        Task<TokenResultDto> Authenticate(ApplicationUser user, CancellationToken cancellationToken);
    }
}