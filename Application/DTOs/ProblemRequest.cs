using Domain.Entities;

namespace Application.DTOs;

public record ProblemRequest 
(
    string Title,
    string Difficulty,
    IEnumerable<Guid>  CategoriesId
);