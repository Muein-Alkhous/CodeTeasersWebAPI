using Domain.Entities;

namespace Application.DTOs;

public record AuthenticationResponse(
    User User,
    string Token 
);