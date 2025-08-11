using Domain.Entities;

namespace Domain.Interfaces;

public interface IProblemCategoryRepository : IRepository<ProblemCategory>
{
    Task<IEnumerable<Category>> GetCategoriesByProblemAsync(Guid problemId);
    Task<IEnumerable<Problem>> GetProblemsByCategory(Guid categoryId);
}