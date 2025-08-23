using Application.DTOs;
using Application.DTOs.Request;
using Application.DTOs.Response;

namespace Application.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResponse?> Login(LoginRequest loginRequest);
    Task<AuthenticationResponse?> Register(RegisterRequest registerRequest);
}