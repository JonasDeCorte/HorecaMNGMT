namespace Horeca.Core.Exceptions
{
    public class IsNotOwnerException : Exception
    {
        public IsNotOwnerException() : base("Mentioned owner is not an owner.")
        {
        }

        public static IsNotOwnerException Instance { get; } = new();
    }
}