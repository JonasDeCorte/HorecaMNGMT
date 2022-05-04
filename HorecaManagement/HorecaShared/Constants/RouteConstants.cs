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
            public const string GetAllBookingsByScheduleId = $"{ClassConstants.Schedule}";
            public const string GetPendingBookingCount = $"{ClassConstants.ListCount}";
            public const string GetByBookingNo = $"{ClassConstants.Details}{Slash}{ClassConstants.BookingNo}";
            public const string GetAllBookingsByUserId = $"{ClassConstants.User}{Slash}{ClassConstants.BookingStatus}";
            public const string GetAllBookingsOfStatus = $"{ClassConstants.Schedule}{Slash}{ClassConstants.BookingStatus}";
        }

        public static class DishConstants
        {
            public const string Get = $"{ClassConstants.Restaurant}";
            public const string Post = $"{ClassConstants.Restaurant}";
            public const string Delete = $"";
            public const string Update = $"{ClassConstants.Restaurant}";

            public const string GetIngredientsByDishId = $"{ClassConstants.Ingredients}{Slash}{ClassConstants.Restaurant}";
            public const string AddIngredientToDish = $"{ClassConstants.Ingredients}{Slash}{ClassConstants.Restaurant}";
            public const string EditIngredientFromDish = $"{ClassConstants.Ingredients}{Slash}{ClassConstants.Restaurant}";
            public const string DeleteById = $"{ClassConstants.Ingredients}{Slash}{ClassConstants.Restaurant}";
            public const string GetAll = $"{ClassConstants.All}{Slash}{ClassConstants.Restaurant}";
            public const string GetDishById = $"{ClassConstants.Id}{Slash}{ClassConstants.Restaurant}";
        }

        public static class IngredientConstants
        {
            public const string GetAllIngredientsByRestaurantId = $"{ClassConstants.All}{Slash}{ClassConstants.Restaurant}";
            public const string Post = $"{ClassConstants.Restaurant}";
            public const string Update = $"{ClassConstants.Restaurant}";
            public const string Delete = $"";
            public const string GetById = $"{ClassConstants.Id}{Slash}{ClassConstants.Restaurant}";
        }

        public static class MenuCardConstants
        {
            public const string GetAllMenuCardsByRestaurantId = $"{ClassConstants.All}{Slash}{ClassConstants.Restaurant}";

            public const string GetFullMenuCard = $"{ClassConstants.Menus}{Slash}{ClassConstants.Dishes}{Slash}{ClassConstants.Restaurant}";

            public const string Post = $"{ClassConstants.Restaurant}";
            public const string PostMenu = $"{ClassConstants.Menus}{Slash}{ClassConstants.Restaurant}";
            public const string PostDishes = $"{ClassConstants.Dishes}{Slash}{ClassConstants.Restaurant}";

            public const string Update = $"{ClassConstants.Restaurant}";
            public const string UpdateMenu = $"{ClassConstants.Menus}{Slash}{ClassConstants.Restaurant}";
            public const string UpdateDish = $"{ClassConstants.Dishes}{Slash}{ClassConstants.Restaurant}";

            public const string Delete = $"";
            public const string DeleteMenu = $"{ClassConstants.Menus}{Slash}{ClassConstants.Restaurant}";
            public const string DeleteDish = $"{ClassConstants.Dishes}{Slash}{ClassConstants.Restaurant}";

            public const string GetById = $"{ClassConstants.Restaurant}";
        }

        public static class MenuConstants
        {
            public const string GetAllByRestaurantId = $"{ClassConstants.All}{Slash}{ClassConstants.Restaurant}";
            public const string Post = $"{ClassConstants.Restaurant}";
            public const string Update = $"{ClassConstants.Restaurant}";

            public const string Delete = $"";
            public const string GetById = $"{ClassConstants.Restaurant}";
            public const string PostDishes = $"{ClassConstants.Dishes}{Slash}{ClassConstants.Restaurant}";
            public const string UpdateDish = $"{ClassConstants.Dishes}{Slash}{ClassConstants.Restaurant}";
            public const string DeleteDish = $"{ClassConstants.Dishes}{Slash}{ClassConstants.Restaurant}";
            public const string GetDishes = $"{ClassConstants.Dishes}{Slash}{ClassConstants.Restaurant}";
        }

        public static class OrderConstants
        {
            public const string Post = $"{ClassConstants.Restaurant}";

            public const string Get = $"{ClassConstants.Table}{Slash}{ClassConstants.Details}";
            public const string GetOrderState = $"{ClassConstants.Restaurant}";
            public const string DeliverOrder = $"{ClassConstants.Restaurant}";
            public const string PrepareOrderLine = $"{ClassConstants.Restaurant}{Slash}{ClassConstants.OrderLine}{Slash}{ClassConstants.Prepare}";
            public const string ReadyOrderLine = $"{ClassConstants.Restaurant}{Slash}{ClassConstants.OrderLine}{Slash}{ClassConstants.Ready}";
        }

        public static class PermissionConstants
        {
            public const string GetById = $"{ClassConstants.Id}";
        }

        public static class RestaurantConstants
        {
            public const string GetByUser = $"{ClassConstants.User}";
            public const string GetById = $"{ClassConstants.Id}";
            public const string Delete = $"{ClassConstants.Id}";
            public const string PostEmployee = $"{ClassConstants.Employee}";
            public const string DeleteEmployee = $"{ClassConstants.Employee}";
        }

        public static class ScheduleConstants
        {
            public const string GetAllByRestaurantId = $"{ClassConstants.All}{Slash}{ClassConstants.Restaurant}";
            public const string Post = $"{ClassConstants.Restaurant}";
            public const string Update = $"{ClassConstants.Restaurant}";
            public const string Delete = $"{ClassConstants.Restaurant}";
            public const string GetById = $"{ClassConstants.Restaurant}";
        }

        public static class TableConstants
        {
            public const string Get = $"{ClassConstants.All}{Slash}{ClassConstants.Floorplan}";
            public const string GetById = $"{ClassConstants.Floorplan}";
            public const string Post = $"{ClassConstants.Schedule}";
            public const string AddTablesFromFloorplan = $"{ClassConstants.Floorplan}";
        }

        public static class FloorplanConstants
        {
            public const string Get = $"{ClassConstants.Restaurant}";
            public const string GetFloorplanById = $"{ClassConstants.Id}{Slash}{ClassConstants.Restaurant}";
            public const string Delete = $"";
        }

        public static class UnitConstants
        {
            public const string GetAllByRestaurantId = $"{ClassConstants.All}{Slash}{ClassConstants.Restaurant}";

            public const string Post = $"{ClassConstants.Restaurant}";
            public const string Update = $"{ClassConstants.Restaurant}";
            public const string Delete = $"";
            public const string GetById = $"{ClassConstants.Restaurant}";
        }
    }
}