using Domain.Entities;

namespace Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> CategoryExistsByTitleAsync(string titles);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(Guid id);
    Task<Category?> GetByTitleAsync(string title);
    Task<IEnumerable<Category>> GetCategoriesByProblemAsync(Guid problemId);
}