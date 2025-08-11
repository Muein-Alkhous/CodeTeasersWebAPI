using Application.DTOs;
using Application.Interfaces;

namespace Application.Services;


public class ProblemCategoryService : IProblemCategoryService
{
    // private readonly AppDbContext _context;
    // private readonly IMapper _mapper;
    //
    // public ProblemCategoryService(AppDbContext context, IMapper mapper)
    // {
    //     _context = context;
    //     _mapper = mapper;
    // }
    //
    // public async Task AssignCategoryToProblemAsync(Guid problemId, Guid categoryId)
    // {
    //     var exists = await _context.ProblemCategories
    //         .AnyAsync(pc => pc.ProblemId == problemId && pc.CategoryId == categoryId);
    //
    //     if (!exists)
    //     {
    //         _context.ProblemCategories.Add(new ProblemCategory
    //         {
    //             ProblemId = problemId,
    //             CategoryId = categoryId
    //         });
    //
    //         await _context.SaveChangesAsync();
    //     }
    // }
    //
    // public async Task RemoveCategoryFromProblemAsync(Guid problemId, Guid categoryId)
    // {
    //     var relation = await _context.ProblemCategories
    //         .FirstOrDefaultAsync(pc => pc.ProblemId == problemId && pc.CategoryId == categoryId);
    //
    //     if (relation != null)
    //     {
    //         _context.ProblemCategories.Remove(relation);
    //         await _context.SaveChangesAsync();
    //     }
    // }
    //
    // public async Task<List<CategoryDto>> GetCategoriesForProblemAsync(Guid problemId)
    // {
    //     var categories = await _context.ProblemCategories
    //         .Where(pc => pc.ProblemId == problemId)
    //         .Include(pc => pc.Category)
    //         .Select(pc => pc.Category)
    //         .ToListAsync();
    //
    //     return _mapper.Map<List<CategoryDto>>(categories);
    // }
    //
    // public async Task<List<ProblemResponse>> GetProblemsForCategoryAsync(Guid categoryId)
    // {
    //     var problems = await _context.ProblemCategories
    //         .Where(pc => pc.CategoryId == categoryId)
    //         .Include(pc => pc.Problem)
    //         .Select(pc => pc.Problem)
    //         .ToListAsync();
    //
    //     return _mapper.Map<List<ProblemResponse>>(problems);
    // }
    public Task AssignCategoryToProblemAsync(Guid problemId, Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCategoryFromProblemAsync(Guid problemId, Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<List<CategoryDto>> GetCategoriesForProblemAsync(Guid problemId)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProblemResponse>> GetProblemsForCategoryAsync(Guid categoryId)
    {
        throw new NotImplementedException();
    }
}
