namespace Application.DTOs.Response;

public record UserResponse
(
    Guid Id,
    string Username,
    string Email,
    UserStatusDto UserStatus
);