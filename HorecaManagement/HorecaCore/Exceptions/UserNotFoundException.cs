namespace Horeca.Core.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User can't be found.")
        {
        }

        public static UserNotFoundException Instance { get; } = new();
    }
}