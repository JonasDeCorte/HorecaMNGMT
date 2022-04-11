using Horeca.MVC.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Horeca.MVC.Services.Handlers
{
    public class HttpTokenHandler : DelegatingHandler
    {
        IHttpContextAccessor httpContextAccessor;
        private readonly ITokenService tokenService;

        public HttpTokenHandler(IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.tokenService = tokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string accessToken = httpContextAccessor.HttpContext.Session.GetString("JWToken");
            request.Headers.Add("Authorization", "Bearer " + accessToken);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var newAccessToken = await tokenService.RefreshTokens();

                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", "Bearer " + newAccessToken);
                var newResponse = await base.SendAsync(request, cancellationToken);

                if (newResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    httpContextAccessor.HttpContext.Session.Remove("Username");
                }
                if (!string.IsNullOrEmpty(newAccessToken))
                {
                    var username = new JwtSecurityTokenHandler().ReadJwtToken(newAccessToken).Claims.Skip(2).First().Value;
                    httpContextAccessor.HttpContext.Session.SetString("Username", username);
                }
                return newResponse;
            }
            return response; ;
        }
    }
}
