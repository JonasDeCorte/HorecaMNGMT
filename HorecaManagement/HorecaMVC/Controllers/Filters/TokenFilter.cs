using Horeca.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Horeca.MVC.Controllers.Filters
{
    public class TokenFilter : ActionFilterAttribute
    {
        public ITokenService tokenService { get; }

        public TokenFilter(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                await tokenService.RefreshTokens();
            }
            finally
            {
                await base.OnActionExecutionAsync(context, next);
            }
        }
    }
}
