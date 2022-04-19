namespace Horeca.Core.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base("bad request.")
        {
        }

        public static BadRequestException Instance { get; } = new();
    }
}