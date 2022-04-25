namespace Horeca.Shared.Constants
{
    public static class RouteConstants
    {
        /// <summary>
        /// Default Uri separator.
        /// </summary>
        private const string Slash = "/";

        public const string Schedule = "Schedule";
        public const string Restaurant = "Restaurant";

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
            public const string GetAllBookingsByScheduleId = $"Schedule{Slash}scheduleId";
            public const string GetPendingBookingCount = "ListCount";
            public const string GetByBookingNo = $"Details{Slash}BookingNo";
            public const string GetAllBookingsByUserId = $"User{Slash}userId{Slash}BookingStatus{Slash}status";
            public const string GetAllBookingsOfStatus = $"Schedule{Slash}scheduleId{Slash}BookingStatus{Slash}status";
        }

        public static class DishConstants
        {
            public const string GetIngredientsByDishId = $"id{Slash}Ingredients{Slash}Restaurant{Slash}restaurantId";
            public const string AddIngredientToDish = $"id{Slash}Ingredients{Slash}Restaurant{Slash}restaurantId";
            public const string EditIngredientFromDish = $"id{Slash}Ingredients{Slash}ingredientId{Slash}Restaurant{Slash}restaurantId";
            public const string DeleteById = $"id{Slash}Ingredients{Slash}ingredientId{Slash}Restaurant{Slash}restaurantId";
            public const string GetAll = $"Restaurant{Slash}restaurantId";
            public const string GetDishById = $"id{Slash}Restaurant{Slash}restaurantId";
        }

        public static class IngredientConstants
        {
            public const string GetAllIngredientsByRestaurantId = $"Restaurant{Slash}restaurantId";
            public const string Post = $"Restaurant{Slash}restaurantId";
            public const string Update = $"id{Slash}Restaurant{Slash}restaurantId";
            public const string Delete = $"id{Slash}Restaurant{Slash}restaurantId";
            public const string GetById = $"id{Slash}Restaurant{Slash}restaurantId";
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