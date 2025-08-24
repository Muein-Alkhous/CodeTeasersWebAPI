using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class CategoryRepository(AppDbContext context) : Repository<Category>(context), ICategoryRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> CategoryExistsByTitleAsync(string title)
    {
        return await _context.Categories.AnyAsync(c => c.Title == title);
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<Category?> GetByTitleAsync(string title)
    {
        return  await _context
            .Categories
            .FirstOrDefaultAsync(c => c.Title == title);
    }

    public async Task<IEnumerable<Category>> GetCategoriesByProblemAsync(Guid problemId)
    {
        return await _context.Categories
            .Where(c => c.ProblemCategories.Any(pc => pc.ProblemId == problemId))
            .ToListAsync();
    }
}