namespace Domain.User
{
    public interface IUser
    {
        string Name { get; }
        string Email { get; }
        string Password { get; }

    }
}
