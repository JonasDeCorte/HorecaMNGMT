using Horeca.MVC.Services.Interfaces;
using Horeca.Shared;
using Horeca.Shared.Constants;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Horeca.MVC.Services
{
    public class TokenService : ITokenService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public TokenService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public void CheckAccessToken(HttpClient httpClient)
        {
            //httpClient.DefaultRequestHeaders.Add("Bearer", httpContextAccessor.HttpContext.Request.Cookies["JWToken"]); 
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                 httpContextAccessor.HttpContext.Request.Cookies["JWToken"]);
        }

        public void SetAccessToken(string accessToken)
        {
            httpContextAccessor.HttpContext.Response.Cookies.Append("JWToken", accessToken);
        }

        public string GetRefreshToken()
        {
            return httpContextAccessor.HttpContext.Request.Cookies["RefreshToken"];
        }

        public void SetRefreshToken(string refreshToken)
        {
            httpContextAccessor.HttpContext.Response.Cookies.Append("RefreshToken", refreshToken);
        }

        public async Task<HttpResponseMessage> RefreshTokens()
        {
            var refreshToken = GetRefreshToken();
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Account}/" +
                $"RefreshToken");
            request.Content = new StringContent(JsonConvert.SerializeObject(refreshToken), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            LoginResult result = JsonConvert.DeserializeObject<LoginResult>(response.Content.ReadAsStringAsync().Result);
            SetAccessToken(result.AccessToken);
            SetRefreshToken(result.RefreshToken);

            return response;
        }
    }
}
