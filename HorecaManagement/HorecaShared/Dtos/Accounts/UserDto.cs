namespace Horeca.Shared.Dtos.Accounts
{
    public class UserDto : BaseUserDto
    {
        public List<PermissionDto>? Permissions { get; set; }
    }

    public class BaseUserDto
    {
        public string Id { get; set; }

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
        public bool IsOwner { get; set; }
    }

    public class RegisterDto
    {
        public string UserId { get; set; }
        public string ErrorMessage { get; set; }
    }
}