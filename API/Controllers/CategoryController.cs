using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase // Not Done
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(category);
    }

    [HttpGet("by-title/{title}")]
    public async Task<IActionResult> GetByTitle(string title)
    {
        var category = await _categoryService.GetCategoryByTitleAsync(title);
        if (category is null)
            return NotFound();
        return Ok(category);
    }

    [HttpPost("{title}")]
    public async Task< IActionResult> Create(string title)
    {
        var category = await _categoryService.CreateCategoryAsync(title);
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult>  Update(Guid id, [FromQuery] string newTitle)
    { 
        var updatedCategory = await _categoryService.UpdateCategoryAsync(id, newTitle);
        return Ok(updatedCategory);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deletedCategory = await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
    
    
    
}