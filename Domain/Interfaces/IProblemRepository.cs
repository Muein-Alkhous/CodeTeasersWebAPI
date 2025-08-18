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
    Task AssignCategoryToProblemAsync(Guid problemId, IEnumerable<Guid> categoryIds);
}