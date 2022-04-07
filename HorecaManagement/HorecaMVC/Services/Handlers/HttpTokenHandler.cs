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
            string accessToken = httpContextAccessor.HttpContext.Request.Cookies["JWToken"];
            request.Headers.Add("Authorization", "Bearer " + accessToken);

            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                httpContextAccessor.HttpContext.Response.Cookies.Delete("Username");
            }
            return response; ;
        }
    }
}
