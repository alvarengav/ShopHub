namespace UserService.Application.DTOs;

public record AuthenticateUserRequest(string Email, string Password);
