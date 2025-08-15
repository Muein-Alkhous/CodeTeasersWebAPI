using Domain.Entities;

namespace Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByTitleAsync(string title);
    Task<IEnumerable<Category>> GetCategoriesByProblemAsync(Guid problemId);
}