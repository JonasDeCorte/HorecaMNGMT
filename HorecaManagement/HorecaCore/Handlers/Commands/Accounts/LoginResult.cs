namespace Horeca.Core.Handlers.Commands.Accounts
{
    public class LoginResult
    {
        public string AccessToken { get; set; }

        public DateTime Expiration { get; set; }
    }
}