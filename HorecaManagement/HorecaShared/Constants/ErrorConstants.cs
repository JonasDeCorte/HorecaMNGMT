namespace Horeca.Shared.Constants
{
    public static class ErrorConstants
    {
        public const string StringLength50 = "{0} may not contain more than 50 characters!";
        public const string StringLength500 = "{0} may not contain more than 500 characters!";
        public const string CheckInSmaller = "Check-in Time must be earlier than Check-out time.";
        public const string StartTimeEarlier = "Start Time must be earlier than End Time.";
        public const string SeatsSmaller = "Available Seats must be lower than or equal to capacity.";
        public const string AboveZero = "{0} must be higher than 0.";
        public const string Invalid = "Invalid {0}";
    }
}
