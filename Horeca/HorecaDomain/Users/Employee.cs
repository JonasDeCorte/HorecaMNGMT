using Domain.Restaurants;


namespace Domain.Users
{
    public class Employee : ApplicationUser, IUser
    {
        public string Name { get; }
        public string Password { get; }
        public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

        public Employee(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

    }
}