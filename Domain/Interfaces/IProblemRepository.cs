using Domain.Entities;

namespace Domain.Interfaces;

public interface IProblemRepository : IRepository<Problem>
{
    Task<Problem?> GetProblemByTitleAsync(string title);
    Task<IEnumerable<Problem>> GetProblemsByCategoryAsync(Guid categoryId);
    Task<IEnumerable<Problem>> GetProblemsByCategoryAsync(string title);
    Task<IEnumerable<Problem>> GetProblemsByDifficultyAsync(string difficulty);
    Task<IEnumerable<Problem>> GetProblemsByUserAsync(Guid userId);
    Task<IEnumerable<Problem>> GetProblemsByUserAsync(string username);
    Task AssignCategoriesToProblemAsync(Guid problemId, IEnumerable<Guid> categoryIds);
    Task AssignTestToProblemAsync(Guid problemId, Guid testId);
    Task AssignDescriptionToProblemAsync(Guid problemId, Guid descriptionId);
    Task AssignTemplateToProblemAsync(Guid problemId, Guid templateId);
}