using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Mapster;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepo;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepo = categoryRepository;
    }


    public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
    {
        var categories =  await _categoryRepo.GetAllAsync();
        return categories.Adapt<IEnumerable<CategoryResponse>>();
    }

    public async Task<CategoryResponse?> GetCategoryByIdAsync(Guid id)
    {
        var category = await _categoryRepo.GetByIdAsync(id);
        return category?.Adapt<CategoryResponse>();
    }

    public async Task<CategoryResponse?> GetCategoryByTitleAsync(string title)
    {
        var category = await _categoryRepo.GetByTitleAsync(title);
        return category.Adapt<CategoryResponse>();
    }

    public async Task<CategoryResponse?> CreateCategoryAsync(string title)
    {
        var categoryExists = await _categoryRepo.GetByTitleAsync(title);
        if (categoryExists != null)
        {
            return null;
        }
        
        var newCategory = new Category
        {
            Title = title,
        };
        await _categoryRepo.AddAsync(newCategory);
        
        return newCategory.Adapt<CategoryResponse>();
    }

    public async Task<CategoryResponse?> UpdateCategoryAsync(Guid id, string title)
    {
        var oldCategory = await _categoryRepo.GetByIdAsync(id);
        if (oldCategory is null)
        {
            return null;
        }
        
        oldCategory.Title = title;
        
        _categoryRepo.Update(oldCategory);
        
        return oldCategory.Adapt<CategoryResponse>();
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var category = await _categoryRepo.GetByIdAsync(id);

        if (category is null)
        {
            return false;
        }

        _categoryRepo.Delete(category);
        
        //// I SHOULD DELETE RELATED <PROBLEMCATEGORIES> HERE 
        
        foreach (var pc in category.ProblemCategories)
        {
            pc.IsDeleted = true;
        }
        
        await _categoryRepo.SaveChangesAsync();
        
        return true;
    }

    public async Task<IEnumerable<CategoryResponse>> GetCategoriesByProblemIdAsync(Guid problemId)
    {
        var categories = await _categoryRepo.GetCategoriesByProblemAsync(problemId);
        return categories.Adapt<IEnumerable<CategoryResponse>>();
    }
    

    
    
}