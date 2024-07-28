using MediatR;

namespace UserService.Application.Commands;

public record AuthenticateUserCommand(string Email, string Password) : IRequest<string>;
