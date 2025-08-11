using Application.DTOs;

namespace Application.Interfaces;

public interface IProblemCategoryService
{
    Task AssignCategoryToProblemAsync(Guid problemId, Guid categoryId);
    Task RemoveCategoryFromProblemAsync(Guid problemId, Guid categoryId);
    Task<List<CategoryDto>> GetCategoriesForProblemAsync(Guid problemId);
    Task<List<ProblemResponse>> GetProblemsForCategoryAsync(Guid categoryId);
}