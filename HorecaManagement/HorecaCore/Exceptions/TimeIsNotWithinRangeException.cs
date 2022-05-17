namespace Horeca.Core.Exceptions
{
    public class TimeIsNotWithinRangeException : Exception
    {
        public TimeIsNotWithinRangeException() : base("Specified time is not within the schedule range.")
        {
        }

        public static TimeIsNotWithinRangeException Instance { get; } = new();
    }
}