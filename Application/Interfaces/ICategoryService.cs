using Application.DTOs;
using Application.DTOs.Response;
using Domain.Entities;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync();
    Task<CategoryResponse?> GetCategoryByIdAsync(Guid id);
    Task<CategoryResponse?> GetCategoryByTitleAsync(string title);
    Task<CategoryResponse> CreateCategoryAsync(string title);
    Task<CategoryResponse?> UpdateCategoryAsync(Guid id, string title);
    Task<CategoryResponse> DeleteCategoryAsync(Guid id);
    Task<IEnumerable<CategoryResponse>> GetCategoriesByProblemIdAsync(Guid problemId);
    
}