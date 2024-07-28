using MediatR;
using UserService.Application.Commands;
using UserService.Application.Services;
using UserService.Domain.Repositories;

namespace UserService.Application.Handlers;

public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthenticateUserCommandHandler(
        IUserRepository userRepository,
        ITokenService jwtService,
        IPasswordHasher passwordHasher
    )
    {
        _userRepository = userRepository;
        _tokenService = jwtService;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> Handle(
        AuthenticateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user is null || _passwordHasher.VerifyPassword(user.Password, request.Password))
            throw new UnauthorizedAccessException("Invalid credentials");

        return _tokenService.GenerateToken(user);
    }
}
