using System.Net.Http.Headers;

namespace Horeca.MVC.Services.Interfaces
{
    public interface ITokenService
    {
        public string GetAccessToken();
        public void SetAccessToken(string accessToken);
        public string GetRefreshToken();
        public void SetRefreshToken(string refreshToken);
        public Task<HttpResponseMessage> RefreshTokens();
    }
}
