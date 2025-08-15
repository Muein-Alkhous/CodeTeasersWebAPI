using Application.DTOs;

namespace Application.Interfaces.Authentication;

public interface IAuthenticationService
{
    AuthenticationResponse Login(LoginRequest loginRequest);
    AuthenticationResponse Register(RegisterRequest registerRequest);
}