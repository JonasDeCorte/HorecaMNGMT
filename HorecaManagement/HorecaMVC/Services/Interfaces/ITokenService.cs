using System.Net.Http.Headers;

namespace Horeca.MVC.Services.Interfaces
{
    public interface ITokenService
    {
        public string GetRefreshToken();
        public void CheckAccessToken(HttpClient httpClient);
        public void SetRefreshToken(string refreshToken);
        public void SetAccessToken(string accessToken);
        public Task<HttpResponseMessage> RefreshTokens();
    }
}
