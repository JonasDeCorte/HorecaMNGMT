namespace Domain.User
{
    public class Employee : IUser
    {
        public string Name { get; }

        public string Email { get; }

    public string Password { get; }

        public Employee(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

    }
}