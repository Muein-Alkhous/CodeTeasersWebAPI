using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ProblemRepository : Repository<Problem>, IProblemRepository
{
    private readonly AppDbContext _context;

    public ProblemRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Problem?> GetProblemByTitleAsync(string title)
    {
        return await _context
            .Problems
            .FirstOrDefaultAsync(e => e.Title == title);
    }

    public async Task<IEnumerable<Problem>> GetProblemsByCategory(Guid categoryId)
    {
        return await _context.Problems
            .Where(p => p.ProblemCategories.Any(pc => pc.CategoryId == categoryId))
            .ToListAsync();
    }

    public async Task AssignCategoryToProblemAsync(Guid problemId, IEnumerable<Guid> categoryIds)
    {
        // Get existing category links to avoid duplicates
        var existingCategoryIds = await _context.ProblemCategories
            .Where(pc => pc.ProblemId == problemId)
            .Select(pc => pc.CategoryId)
            .ToListAsync();

        var newLinks = categoryIds
            .Except(existingCategoryIds) // Avoid duplicates
            .Select(categoryId => new ProblemCategory
            {
                ProblemId = problemId,
                CategoryId = categoryId
            });

        var problemCategories = newLinks.ToList();
        if (problemCategories.Count != 0)
            await _context.ProblemCategories.AddRangeAsync(problemCategories);
    }
}