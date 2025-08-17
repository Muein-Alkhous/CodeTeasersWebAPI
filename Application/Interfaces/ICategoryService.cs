using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync();
    Task<CategoryResponse?> GetCategoryByIdAsync(Guid id);
    Task<CategoryResponse?> GetCategoryByTitleAsync(string title);
    Task<CategoryResponse?> CreateCategoryAsync(string title);
    Task<CategoryResponse?> UpdateCategoryAsync(Guid id, string title);
    Task<bool> DeleteCategoryAsync(Guid id);
    Task<IEnumerable<CategoryResponse>> GetCategoriesByProblemIdAsync(Guid problemId);
    
}