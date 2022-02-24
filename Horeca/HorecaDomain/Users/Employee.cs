using Domain.Restaurants;
using Microsoft.AspNetCore.Identity;

namespace Domain.Users
{
    public class Employee : IdentityUser
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public EmployeeType EmployeeType { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        public Employee()
        {
        }

        public Employee(string name)
        {
            Name = name;
        }

        public Employee(string name, EmployeeType employeeType)
        {
            Name = name;
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