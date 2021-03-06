using System.Net.Http.Headers;

namespace Horeca.MVC.Services.Interfaces
{
    public interface ITokenService
    {
        public void SetAccessToken(string accessToken);
        public string GetRefreshToken();
        public void SetRefreshToken(string refreshToken);
        public Task<string> RefreshTokens();
    }
}
