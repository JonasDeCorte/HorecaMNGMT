using Domain.Restaurants;
using Microsoft.AspNetCore.Identity;

namespace Domain.Users
{
    public class Employee : IdentityUser, IUser
    {
        public string Name { get; }
        public string Password { get; }
        public EmployeeType EmployeeType { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

        public Employee(string name, string email, string password, EmployeeType employeeType)
        {
            Name = name;
            Email = email;
            Password = password;
            EmployeeType = employeeType;
            EmployeeRole = AssignDefaultRole(employeeType);
        }

        private EmployeeRole AssignDefaultRole(EmployeeType employeeType)
        {
            switch (employeeType)
            {
                case EmployeeType.HALL: return EmployeeRole.DefaultHall;
                case EmployeeType.KITCHEN: return EmployeeRole.DefaultKitchen;
                case EmployeeType.OWNER: return EmployeeRole.DefaultOwner;
                default: return EmployeeRole.DefaultHall;
            }
        }
    }
}