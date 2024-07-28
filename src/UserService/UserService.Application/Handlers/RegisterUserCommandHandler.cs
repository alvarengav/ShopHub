using MediatR;
using UserService.Application.Commands;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;
using UserService.Domain.ValueObjects;

namespace UserService.Application.Handlers;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(
            new UserId(Guid.NewGuid()),
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        await _userRepository.AddUserAsync(user);
        return user.Id.Value;
    }
}
