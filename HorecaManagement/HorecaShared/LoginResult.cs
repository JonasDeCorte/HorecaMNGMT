namespace Horeca.Shared
{
    public class LoginResult
    {   /// <summary>
        /// The Access token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The refresh token.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}