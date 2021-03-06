using Horeca.Core.Exceptions;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Data.Services;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.Tokens;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Horeca.Core.Handlers.Commands.Accounts
{
    public class LoginCommand : IRequest<TokenResultDto>
    {
        public LoginCommand(LoginUserDto model)
        {
            Model = model;
        }

        public LoginUserDto Model { get; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResultDto>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IAuthenticateService authenticateService;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAuthenticateService authenticateService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authenticateService = authenticateService;
        }

        public async Task<TokenResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to login {object} with username: {username}", nameof(ApplicationUser), request.Model.Username);

            var user = await userManager.FindByNameAsync(request.Model.Username);

            if (user == null)
            {
                logger.Error(UserNotFoundException.Instance);
                return new TokenResultDto()
                {
                    ErrorMessage = UserNotFoundException.Instance.Message
                };
                //throw new UserNotFoundException();
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, request.Model.Password, false, false);

            if (!signInResult.Succeeded)
            {
                logger.Error(SignInException.Instance);
                return new TokenResultDto()
                {
                    ErrorMessage = SignInException.Instance.Message
                };
                //throw new SignInException();
            }
            logger.Info("Authenticating {object} with username: {username}", nameof(ApplicationUser), request.Model.Username);

            var result = await authenticateService.Authenticate(user, cancellationToken);

            return new TokenResultDto()
            {
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken
            };
        }
    }
}