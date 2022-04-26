namespace Horeca.Core.Exceptions
{
    public class StringIsNullOrEmptyException : Exception
    {
        public StringIsNullOrEmptyException() : base("string is null or empty.")
        {
        }

        public static StringIsNullOrEmptyException Instance { get; } = new();
    }
}