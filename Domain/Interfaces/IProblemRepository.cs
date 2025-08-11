using Domain.Entities;

namespace Domain.Interfaces;

public interface IProblemRepository : IRepository<Problem>
{
    Task<Problem> GetProblemByTitleAsync(string title);
    Task<IEnumerable<Problem>> GetProblemsByUserAsync(Guid userId);
    Task<IEnumerable<Problem>> GetProblemsByUserAndCategoryAsync(Guid userId, Guid categoryId);
}