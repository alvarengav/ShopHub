using MediatR;

namespace UserService.Application.Commands;

public record RegisterUserCommand(string FirstName, string LastName, string Email, string Password)
    : IRequest<Guid>;
