using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Tokens;
using Newtonsoft.Json;
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

        public void SetAccessToken(string accessToken)
        {
            httpContextAccessor.HttpContext.Session.SetString("JWToken", accessToken);
        }

        public void SetRefreshToken(string refreshToken)
        {
            httpContextAccessor.HttpContext.Session.SetString("RefreshToken", refreshToken);
        }

        public string GetRefreshToken()
        {
            return httpContextAccessor.HttpContext.Session.GetString("RefreshToken");
        }

        public async Task<string> RefreshTokens()
        {
            RefreshTokenDto refreshTokenDto = new()
            {
                RefreshToken = GetRefreshToken()
            };
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Account}/{ClassConstants.RefreshToken}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(refreshTokenDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            TokenResultDto result = JsonConvert.DeserializeObject<TokenResultDto>(response.Content.ReadAsStringAsync().Result);
            SetAccessToken(result.AccessToken);
            SetRefreshToken(result.RefreshToken);
            return result.AccessToken;
        }
    }
}