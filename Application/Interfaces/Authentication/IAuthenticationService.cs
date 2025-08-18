using Application.DTOs;

namespace Application.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResponse?> Login(LoginRequest loginRequest);
    Task<AuthenticationResponse?> Register(RegisterRequest registerRequest);
}