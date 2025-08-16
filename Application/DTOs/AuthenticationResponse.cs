using Domain.Entities;

namespace Application.DTOs;

public record AuthenticationResponse(
    UserResponse User,
    string Token 
);