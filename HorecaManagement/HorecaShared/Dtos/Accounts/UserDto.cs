using Horeca.Shared.Data.Entities.Account;

namespace Horeca.Shared.Dtos.Accounts
{
    public class UserDto
    {
        public string Username { get; set; }

        public List<Tuple<string, string>> Permissions { get; set; }
    }

    public class BaseUserDto
    {
        public string Username { get; set; }
    }

    public class LoginUserDto

    {
        public string? Username { get; set; }

        public string? Password { get; set; }
    }

    public class RegisterUserDto
    {
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}