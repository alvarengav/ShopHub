using UserService.Domain.Entities;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(UserId id, CancellationToken cancellationToken = default);
    Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task AddUserAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(User user, CancellationToken cancellationToken = default);
}
