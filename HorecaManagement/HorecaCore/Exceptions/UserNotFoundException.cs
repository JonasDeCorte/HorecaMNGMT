namespace Horeca.Core.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User does not exist")
        {
        }

        public static UserNotFoundException Instance { get; } = new();
    }
}