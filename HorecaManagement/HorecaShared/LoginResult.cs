namespace Horeca.Shared
{
    public class LoginResult
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime Expiration { get; set; }
    }
}