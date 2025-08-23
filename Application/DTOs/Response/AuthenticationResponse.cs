namespace Application.DTOs.Response;

public record AuthenticationResponse(
    UserResponse User,
    string Token 
);