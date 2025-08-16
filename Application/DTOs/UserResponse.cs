using Domain.Entities;

namespace Application.DTOs;

public record UserResponse
(
    Guid Id,
    string Username,
    string Email,
    UserStatusDto UserStatus
);