namespace Horeca.Shared.Constants
{
    public static class RouteConstants
    {
        /// <summary>
        /// Default Uri separator.
        /// </summary>
        private const string Slash = "/";

        public static class AccountConstants
        {
            public const string LoginSuperAdmin = $"{ClassConstants.SuperAdminAccessToken}";
            public const string GetUserClaims = $"{ClassConstants.GetUserClaims}";
            public const string RefreshToken = $"{ClassConstants.RefreshToken}";
            public const string RevokeToken = $"{ClassConstants.RefreshToken}{Slash}{ClassConstants.Revoke}";
            public const string Login = $"{ClassConstants.Login}";
            public const string Register = $"{ClassConstants.Register}";
            public const string RegisterAdmin = $"{ClassConstants.RegisterAdmin}";
            public const string UserPermissions = $"{ClassConstants.User}{Slash}{ClassConstants.UserPermissions}";
            public const string GetAllUsers = $"{ClassConstants.Users}";
        }

        public static class BookingConstants
        {
            public const string GetAllBookingsByScheduleId = $"{ClassConstants.Schedule}{Slash}{ClassConstants.ScheduleId}";
            public const string GetPendingBookingCount = $"{ClassConstants.ListCount}";
            public const string GetByBookingNo = $"{ClassConstants.Details}{Slash}{ClassConstants.BookingNo}";
            public const string GetAllBookingsByUserId = $"{ClassConstants.User}{Slash}{ClassConstants.UserId}{Slash}{ClassConstants.BookingStatus}{Slash}{ClassConstants.Status}";
            public const string GetAllBookingsOfStatus = $"{ClassConstants.Schedule}{Slash}{ClassConstants.ScheduleId}{Slash}{ClassConstants.BookingStatus}{Slash}{ClassConstants.Status}";
        }

        public static class DishConstants
        {
            public const string Get = $"{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string Post = $"{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string Delete = $"{ClassConstants.Id}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string Update = $"{ClassConstants.Id}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";

            public const string GetIngredientsByDishId = $"{ClassConstants.Id}{Slash}{ClassConstants.Ingredients}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string AddIngredientToDish = $"{ClassConstants.Id}{Slash}{ClassConstants.Ingredients}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string EditIngredientFromDish = $"{ClassConstants.Id}{Slash}{ClassConstants.Ingredients}{Slash}{ClassConstants.IngredientId}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string DeleteById = $"{ClassConstants.Id}{Slash}{ClassConstants.Ingredients}{Slash}{ClassConstants.IngredientId}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string GetAll = $"{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string GetDishById = $"{ClassConstants.Id}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
        }

        public static class IngredientConstants
        {
            public const string GetAllIngredientsByRestaurantId = $"{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string Post = $"{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string Update = $"{ClassConstants.Id}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string Delete = $"{ClassConstants.Id}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string GetById = $"{ClassConstants.Id}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
        }

        public static class MenuCardConstants
        {
            public const string GetAllMenuCardsByRestaurantId = $"{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";

            public const string GetFullMenuCard = $"{ClassConstants.Id}{Slash}{ClassConstants.Menus}{Slash}{ClassConstants.Dishes}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";

            public const string Post = $"{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string PostMenu = $"{ClassConstants.Id}{Slash}{ClassConstants.Menus}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string PostDishes = $"{ClassConstants.Id}{Slash}{ClassConstants.Dishes}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";

            public const string Update = $"{ClassConstants.Id}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string UpdateMenu = $"{ClassConstants.Id}{Slash}{ClassConstants.Menus}{Slash}{ClassConstants.MenuId}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string UpdateDish = $"{ClassConstants.Id}{Slash}{ClassConstants.Dishes}{Slash}{ClassConstants.DishId}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";

            public const string Delete = $"{ClassConstants.Id}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string DeleteMenu = $"{ClassConstants.Id}{Slash}{ClassConstants.Menus}{Slash}{ClassConstants.MenuId}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
            public const string DeleteDish = $"{ClassConstants.Id}{Slash}{ClassConstants.Dishes}{Slash}{ClassConstants.DishId}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";

            public const string GetById = $"{ClassConstants.Id}{Slash}{ClassConstants.Restaurant}{Slash}{ClassConstants.RestaurantId}";
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