using UserService.Domain.Entities;

namespace UserService.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
