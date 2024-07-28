using MediatR;
using UserService.Application.DTOs;
using UserService.Domain.ValueObjects;

namespace UserService.Application.Commands;

public record GetAuthenticatedUserInfoQuery(UserId UserId, string Email) : IRequest<UserDTO>;
