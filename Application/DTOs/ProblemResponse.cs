using Domain.Entities;

namespace Application.DTOs;

public record ProblemResponse
(
    Guid Id,
    string Title,
    string Difficulty,
    IEnumerable<CategoryResponse> CategoryResponses
);
