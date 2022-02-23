namespace Domain.Users
{
    public interface IUser
    {
        string Name { get; }
        string Email { get; }
        string Password { get; }

    }
}
