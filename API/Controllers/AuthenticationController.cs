using Application.DTOs;
using Application.DTOs.Request;
using Application.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var authResult = await _authenticationService.Register(registerRequest);
        if (authResult == null)
        {
            return BadRequest(new
            {
                type = "http://tools.ietf.org/html/rfc7231#section-6.5.1",
                title = "Bad Request",
                status = 400,
                detail = $"User with given username: '{registerRequest.Username}' already exists"
            });
        }
        return Ok(authResult);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var authResult = await _authenticationService.Login(loginRequest);
        if (authResult == null)
        {
            return NotFound(new
            {
                type = "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                title = "Not Found",
                status = 404,
                detail = $"User with given username: '{loginRequest.Username}' not found"
            });
        }
        return Ok(authResult);
    }
}
