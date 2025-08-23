namespace Application.DTOs.Request;

public record ProblemRequest 
(
    string Title,
    string Difficulty,
    IEnumerable<Guid>  CategoriesId
);