namespace Application.DTOs.Request;

public record LoginRequest
(
    string Username,
    string Password
);