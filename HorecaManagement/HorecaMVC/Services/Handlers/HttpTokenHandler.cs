namespace Horeca.MVC.Services.Handlers
{
    public class HttpTokenHandler : DelegatingHandler
    {
        IHttpContextAccessor httpContextAccessor;

        public HttpTokenHandler(IHttpContextAccessor httpContextAccessor)
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
