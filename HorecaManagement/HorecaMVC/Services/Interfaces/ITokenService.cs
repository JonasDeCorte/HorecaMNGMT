namespace Horeca.MVC.Services.Interfaces
{
    public interface ITokenService
    {
        public string GetRefreshToken();
        public void CheckAccessToken();
        public void SetRefreshToken(string refreshToken);
        public void SetAccessToken(string accessToken);
        public Task<HttpResponseMessage> RefreshTokens();
    }
}
