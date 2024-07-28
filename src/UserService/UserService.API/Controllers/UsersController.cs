using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands;
using UserService.Application.DTOs;
using UserService.Domain.ValueObjects;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new RegisterUserCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        var userId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Register), new { id = userId }, userId);
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var command = new AuthenticateUserCommand(request.Email, request.Password);
            var token = await _mediator.Send(command);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid credentials");
        }
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetAuthenticatedUserInfo()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);

        if (userId is null || email is null)
            return Unauthorized();

        var query = new GetAuthenticatedUserInfoQuery(new UserId(Guid.Parse(userId)), email);
        var userInfo = await _mediator.Send(query);
        if (userInfo is null)
            return Unauthorized();

        return Ok(userInfo);
    }
}
