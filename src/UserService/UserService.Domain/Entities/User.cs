using UserService.Domain.ValueObjects;

namespace UserService.Domain.Entities;

public class User : Entity<UserId>
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;

    private User() { }

    public User(UserId userId, string firstName, string lastName, string email, string password)
    {
        Id = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }
}
