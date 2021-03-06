namespace Horeca.Shared.Constants
{
    public static class ErrorConstants
    {
        public const string StringLength50 = "{0} may not contain more than 50 characters!";
        public const string StringLength500 = "{0} may not contain more than 500 characters!";
        public const string CheckInSmaller = "Check-in Time must be earlier than Check-out time.";
        public const string StartTimeEarlier = "Start Time must be earlier than End Time.";
        public const string SeatsSmaller = "Available Seats must be lower than or equal to capacity.";
        public const string PeopleAmountSmaller = "Amount of people must be lower than or equal to available seats.";
        public const string AboveZero = "{0} must be higher than 0.";
        public const string Invalid = "Invalid {0}";
        public const string AmountOfPerson = "Entered amount exceeds total capacity.";

        public const string Password = "Error occured while signing in user, password incorrect";
        public const string Username = "User does not exist";
        public const string ExceedSeats = "exceeded the amount of available seats...";
        public const string Register = "Error occured while registering user.";
    }
}