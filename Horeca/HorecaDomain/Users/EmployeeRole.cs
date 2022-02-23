namespace Domain.Users
{
    [Flags]
    public enum EmployeeRole
    {
        INGREDIENT = 1,
        DISH = 2,
        MENU = 4,
        MENUCARD = 8,
        TABLE = 16,
        FLOORPLAN = 32,
        BOOKING = 64,
        ORDER = 128,
    }
}
