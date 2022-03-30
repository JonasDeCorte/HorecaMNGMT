using Horeca.Core.Exceptions;
using Horeca.Shared.Data.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Horeca.Core.Handlers.Commands.Accounts
{
    public class RevokeTokenCommand : IRequest<int>
    {
        public RevokeTokenCommand(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }

    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, int>
    {
        private readonly IApplicationDbContext context;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public RevokeTokenCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to revoke refresh token : {token}", request.Token);

            var token = await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token.Equals(request.Token), cancellationToken: cancellationToken);

            if (token == null)
            {
                logger.Error(InvalidRefreshTokenException.Instance);
                throw new InvalidRefreshTokenException();
            }

            logger.Info("removing refresh token : {token}", token);

            context.RefreshTokens.Remove(token);
            await context.SaveChangesAsync(cancellationToken);

            return token.Id;
        }
    }
}