using MediatR;
using UserService.Application.Commands;
using UserService.Application.DTOs;
using UserService.Domain.Repositories;

namespace UserService.Application.Handlers;

public class GetAuthenticatedUserInfoQueryHandler
    : IRequestHandler<GetAuthenticatedUserInfoQuery, UserDTO?>
{
    private readonly IUserRepository _userRepository;

    public GetAuthenticatedUserInfoQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDTO?> Handle(
        GetAuthenticatedUserInfoQuery request,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email);

        if (user is null)
            return null;

        return new UserDTO(user.Id.Value.ToString(), user.FirstName, user.LastName, user.Email);
    }
}
