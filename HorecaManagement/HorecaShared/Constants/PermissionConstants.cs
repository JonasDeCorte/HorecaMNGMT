using Horeca.Shared.AuthUtils;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeca.Shared.Constants
{
    public static class PermissionConstants
    {
        // unit perms

        public const string Unit_Read = $"{nameof(Unit)}_{Permissions.Read}";
        public const string Unit_Create = $"{nameof(Unit)}_{Permissions.Create}";
        public const string Unit_Update = $"{nameof(Unit)}_{Permissions.Update}";
        public const string Unit_Delete = $"{nameof(Unit)}_{Permissions.Delete}";

        // Ingredient perms

        public const string Ingredient_Read = $"{nameof(Ingredient)}_{Permissions.Read}";
        public const string Ingredient_Create = $"{nameof(Ingredient)}_{Permissions.Create}";
        public const string Ingredient_Update = $"{nameof(Ingredient)}_{Permissions.Update}";
        public const string Ingredient_Delete = $"{nameof(Ingredient)}_{Permissions.Delete}";

        // Dish perms

        public const string Dish_Read = $"{nameof(Dish)}_{Permissions.Read}";
        public const string Dish_Create = $"{nameof(Dish)}_{Permissions.Create}";
        public const string Dish_Update = $"{nameof(Dish)}_{Permissions.Update}";
        public const string Dish_Delete = $"{nameof(Dish)}_{Permissions.Delete}";

        // Menu perms

        public const string Menu_Read = $"{nameof(Menu)}_{Permissions.Read}";
        public const string Menu_Create = $"{nameof(Menu)}_{Permissions.Create}";
        public const string Menu_Update = $"{nameof(Menu)}_{Permissions.Update}";
        public const string Menu_Delete = $"{nameof(Menu)}_{Permissions.Delete}";

        // MenuCard perms

        public const string MenuCard_Read = $"{nameof(MenuCard)}_{Permissions.Read}";
        public const string MenuCard_Create = $"{nameof(MenuCard)}_{Permissions.Create}";
        public const string MenuCard_Update = $"{nameof(MenuCard)}_{Permissions.Update}";
        public const string MenuCard_Delete = $"{nameof(MenuCard)}_{Permissions.Delete}";

        // Restaurant perms

        public const string Restaurant_Read = $"{nameof(Restaurant)}_{Permissions.Read}";
        public const string Restaurant_Create = $"{nameof(Restaurant)}_{Permissions.Create}";
        public const string Restaurant_Update = $"{nameof(Restaurant)}_{Permissions.Update}";
        public const string Restaurant_Delete = $"{nameof(Restaurant)}_{Permissions.Delete}";

        // Schedule perms

        public const string Schedule_Read = $"{nameof(Schedule)}_{Permissions.Read}";
        public const string Schedule_Create = $"{nameof(Schedule)}_{Permissions.Create}";
        public const string Schedule_Update = $"{nameof(Schedule)}_{Permissions.Update}";
        public const string Schedule_Delete = $"{nameof(Schedule)}_{Permissions.Delete}";

        // Booking perms

        public const string Booking_Read = $"{nameof(Booking)}_{Permissions.Read}";
        public const string Booking_Create = $"{nameof(Booking)}_{Permissions.Create}";
        public const string Booking_Update = $"{nameof(Booking)}_{Permissions.Update}";
        public const string Booking_Delete = $"{nameof(Booking)}_{Permissions.Delete}";

        // Table perms

        public const string Table_Read = $"{nameof(Table)}_{Permissions.Read}";
        public const string Table_Create = $"{nameof(Table)}_{Permissions.Create}";
        public const string Table_Update = $"{nameof(Table)}_{Permissions.Update}";
        public const string Table_Delete = $"{nameof(Table)}_{Permissions.Delete}";

        // Floorplan perms

        public const string Floorplan_Read = $"{nameof(Floorplan)}_{Permissions.Read}";
        public const string Floorplan_Create = $"{nameof(Floorplan)}_{Permissions.Create}";
        public const string Floorplan_Update = $"{nameof(Floorplan)}_{Permissions.Update}";
        public const string Floorplan_Delete = $"{nameof(Floorplan)}_{Permissions.Delete}";

        // Order perms

        public const string Order_Read = $"{nameof(Order)}_{Permissions.Read}";
        public const string Order_Create = $"{nameof(Order)}_{Permissions.Create}";
        public const string Order_Update = $"{nameof(Order)}_{Permissions.Update}";
        public const string Order_Delete = $"{nameof(Order)}_{Permissions.Delete}";

        // Permission perms

        public const string Permission_Read = $"{nameof(Permission)}_{Permissions.Read}";
        public const string Permission_Create = $"{nameof(Permission)}_{Permissions.Create}";
        public const string Permission_Update = $"{nameof(Permission)}_{Permissions.Update}";
        public const string Permission_Delete = $"{nameof(Permission)}_{Permissions.Delete}";

        // ApplicationUser perms

        public const string ApplicationUser_Read = $"{nameof(ApplicationUser)}_{Permissions.Read}";
        public const string ApplicationUser_Create = $"{nameof(ApplicationUser)}_{Permissions.Create}";
        public const string ApplicationUser_Update = $"{nameof(ApplicationUser)}_{Permissions.Update}";
        public const string ApplicationUser_Delete = $"{nameof(ApplicationUser)}_{Permissions.Delete}";
        public const string NewUser = $"{nameof(ApplicationUser)}_NewUser";
    }
}