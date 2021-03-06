using Horeca.Core.Exceptions;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Data.Services;
using Horeca.Shared.Dtos.Tokens;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Horeca.Core.Handlers.Commands.Accounts
{
    public class RefreshCommand : IRequest<TokenResultDto>
    {
        public RefreshCommand(RefreshTokenDto model)
        {
            Model = model;
        }

        public RefreshTokenDto Model { get; }
    }

    public class RefreshCommandHandler : IRequestHandler<RefreshCommand, TokenResultDto>
    {
        private readonly IAuthenticateService authenticateService;
        private readonly IRefreshTokenValidator refreshTokenValidator;
        private readonly IApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public RefreshCommandHandler(IRefreshTokenValidator refreshTokenValidator, IApplicationDbContext context,
            UserManager<ApplicationUser> userManager, IAuthenticateService authenticateService)
        {
            this.refreshTokenValidator = refreshTokenValidator;
            this.context = context;
            this.userManager = userManager;
            this.authenticateService = authenticateService;
        }

        public async Task<TokenResultDto> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            logger.Info("requesting a refresh with token:  {username}", request.Model.RefreshToken);

            var isValidRefreshToken = refreshTokenValidator.Validate(request.Model.RefreshToken);
            if (!isValidRefreshToken)
            {
                logger.Error(InvalidRefreshTokenException.Instance);
                throw new InvalidRefreshTokenException();
            }

            var refreshToken =
                await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == request.Model.RefreshToken,
                    cancellationToken);

            if (refreshToken is null)
            {
                logger.Error(InvalidRefreshTokenException.Instance);
                throw new InvalidRefreshTokenException();
            }

            context.RefreshTokens.Remove(refreshToken);
            await context.SaveChangesAsync(cancellationToken);

            var user = await userManager.FindByIdAsync(refreshToken.UserId);
            if (user is null) throw new UserNotFoundException();

            return await authenticateService.Authenticate(user, cancellationToken);
        }
    }
}