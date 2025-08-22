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

    public async Task<List<Problem>> GetAllProblemsAsync()
    {
        return await _context .Problems
            .Include(p => p.ProblemCategories)
            .ThenInclude(pc => pc.Category)
            .ToListAsync();
    }

    public async Task<Problem?> GetProblemByIdAsync(Guid id)
    {
        return await _context.Problems
            .Include(p => p.ProblemCategories)
            .ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Problem?> GetProblemByTitleAsync(string title)
    {
        return await _context
            .Problems
            .Include(p => p.ProblemCategories)
            .ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(e => e.Title == title);
    }

    public async Task<List<Category>> GetCategoriesFromProblemAsync(Guid problemId)
    {
        return await _context.Problems
            .Where(p => p.Id == problemId)
            .SelectMany(p => p.ProblemCategories)
            .Select(pc => pc.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Problem>> GetProblemsByCategoryAsync(Guid categoryId)
    {
        return await _context.Problems
            .Where(p => p.ProblemCategories.Any(pc => pc.CategoryId == categoryId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Problem>> GetProblemsByCategoryAsync(string title)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Title == title);
        return await _context.Problems
            .Where(p => p.ProblemCategories.Any(pc => pc.CategoryId == category!.Id))
            .ToListAsync();
    }

    public async Task<IEnumerable<Problem>> GetProblemsByDifficultyAsync(string difficulty)
    {
        return await _context.Problems
            .Where(p => p.Difficulty == difficulty)
            .ToListAsync();
    }

    public async Task<IEnumerable<Problem>> GetProblemsByUserAsync(Guid userId)
    {
        return await _context.Problems
            .Where(p => p.Submissions.Any(s => s.UserId == userId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Problem>> GetProblemsByUserAsync(string username)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
        return await _context.Problems
            .Where(p => p.Submissions.Any(s => s.UserId == user!.Id))
            .ToListAsync();
    }


    public async Task AssignCategoriesToProblemAsync(Guid problemId, IEnumerable<Guid> categoryIds)
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

    public async Task AssignTestToProblemAsync(Guid problemId, Guid testId)
    {
        throw new NotImplementedException();
    }

    public async Task AssignDescriptionToProblemAsync(Guid problemId, Guid descriptionId)
    {
        throw new NotImplementedException();
    }

    public async Task AssignTemplateToProblemAsync(Guid problemId, Guid templateId)
    {
        throw new NotImplementedException();
    }
}