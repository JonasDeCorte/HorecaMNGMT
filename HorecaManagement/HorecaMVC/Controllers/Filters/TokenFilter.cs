using Microsoft.AspNetCore.Mvc.Filters;

namespace Horeca.MVC.Controllers.Filters
{
    public class TokenFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Cookies["JWToken"];
            context.HttpContext.Request.Headers.Add("Authorization", "Bearer " + token);
        }
    }
}
