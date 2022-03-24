using Horeca.Shared.Data.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeca.Shared.Data.Services
{
    /// <summary>
    /// Interface for generating token.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates token based on user information.
        /// </summary>
        /// <param name="user"><see cref="User"/> instance.</param>
        /// <returns>Generated token.</returns>
        string Generate(ApplicationUser user);
    }
}