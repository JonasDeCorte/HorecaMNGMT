namespace Horeca.Core.Exceptions
{
    public class RegisterException : Exception
    {
        public RegisterException() : base("Error occured while registering user.")
        {
        }

        public static RegisterException Instance { get; } = new();
    }
}