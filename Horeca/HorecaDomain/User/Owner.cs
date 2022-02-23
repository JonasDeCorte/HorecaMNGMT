
namespace Domain.User
{
    public class Owner : IUser
    {
        public string Name { get; }

        public string Email { get; }

        public string Password { get; }

        public Owner(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
