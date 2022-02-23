using Domain.Restaurants;


namespace Domain.Users
{
    public class Employee : ApplicationUser, IUser
    {
        public string Name { get; }
        public string Password { get; }
        public EmployeeType EmployeeType { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

        public Employee(string name, string email, string password, EmployeeRole employeeRole, EmployeeType employeeType)
        {
            Name = name;
            Email = email;
            Password = password;
            EmployeeType = employeeType;
            EmployeeRole = employeeRole;
        }

    }
}