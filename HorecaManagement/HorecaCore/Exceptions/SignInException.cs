namespace Horeca.Core.Exceptions
{
    public class SignInException : Exception
    {
        public SignInException() : base("Error occured while signing in user")
        {
        }

        public static SignInException Instance { get; } = new();
    }
}