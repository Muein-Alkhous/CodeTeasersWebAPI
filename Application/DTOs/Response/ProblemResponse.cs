using Domain.Entities;

namespace Application.DTOs.Response;

public record ProblemResponse
(
    Guid Id,
    string Title,
    string Difficulty,
    IEnumerable<CategoryResponse> CategoryResponses,
    Description Description
);
