using Domain.Entities;

namespace Domain.Interfaces;

public interface IProblemRepository : IRepository<Problem>
{
    Task<Problem?> GetProblemByTitleAsync(string title);
    Task<IEnumerable<Problem>> GetProblemsByCategory(Guid categoryId);
    Task AssignCategoryToProblemAsync(Guid problemId, IEnumerable<Guid> categoryIds);
}