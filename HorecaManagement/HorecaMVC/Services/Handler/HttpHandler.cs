using Horeca.MVC.Services.Interfaces;

namespace HorecaMVC.Services.Handler
{
    public class HttpHandler : DelegatingHandler
    {
        IHttpContextAccessor httpContextAccessor;

        public HttpHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", "Bearer " + httpContextAccessor.HttpContext.Request.Cookies["JWToken"]);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
