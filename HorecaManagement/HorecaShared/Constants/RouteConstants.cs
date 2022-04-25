namespace Horeca.Shared.Constants
{
    public static class RouteConstants
    {
        /// <summary>
        /// Default Uri separator.
        /// </summary>
        private const string Slash = "/";

        public const string Schedule = "Schedule";

        public static class AccountConstants
        {
            public const string LoginSuperAdmin = "SuperAdminAccessToken";
            public const string GetUserClaims = "GetUserClaims";
            public const string RefreshToken = "RefreshToken";
            public const string RevokeToken = $"RefreshToken{Slash}revoke";
            public const string Login = "login";
            public const string Register = "register";
            public const string RegisterAdmin = "register-admin";
            public const string UserPermissions = $"User{Slash}UserPermissions";
            public const string GetAllUsers = $"Users";
        }

        public static class BookingConstants
        {
            public const string GetPendingBookingCount = "ListCount";
            public const string GetByBookingNo = $"Details{Slash}BookingNo";
            public const string GetAllBookingsByUserId = $"User{Slash}BookingStatus";
            public const string GetAllBookingsOfStatus = $"Schedule{Slash}BookingStatus";
        }

        public static class DishConstants
        {
        }

        public static class IngredientConstants
        {
        }

        public static class MenuCardConstants
        {
        }

        public static class MenuConstants
        {
        }

        public static class OrderConstants
        {
        }

        public static class PermissionConstants
        {
        }

        public static class RestaurantConstants
        {
        }

        public static class ScheduleConstants
        {
        }

        public static class TableConstants
        {
        }

        public static class UnitConstants
        {
        }
    }
}