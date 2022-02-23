using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{   [Flags]
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
