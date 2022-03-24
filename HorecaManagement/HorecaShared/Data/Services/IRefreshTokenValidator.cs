using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeca.Shared.Data.Services
{
    /// <summary>
    /// Interface for validating refresh token.
    /// </summary>
    public interface IRefreshTokenValidator
    {
        /// <summary>
        /// Validates refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>True if token is valid,otherwise false.</returns>
        bool Validate(string refreshToken);
    }
}