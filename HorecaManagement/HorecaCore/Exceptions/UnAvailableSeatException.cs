namespace Horeca.Core.Exceptions
{
    public class UnAvailableSeatException : Exception
    {
        public UnAvailableSeatException() : base("exceeded the amount of available seats...")
        {
        }

        public static UnAvailableSeatException Instance { get; } = new();
    }
}