using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
        if (category is null)
            return NotFound();
        return Ok(category);
    }

    [HttpGet("{title:alpha}")]
    public async Task<IActionResult> GetByTitle(string title)
    {
        var category = await _categoryService.GetCategoryByTitleAsync(title);
        if (category is null)
            return NotFound();
        return Ok(category);
    }

    [HttpPost("{title:alpha}")]
    public async Task< IActionResult> Create(string title)
    {
        var category = await _categoryService.CreateCategoryAsync(title);
        if (category is null)
        {
            return BadRequest();
        }
        return Created();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult>  Update(Guid id, [FromQuery] string newTitle)
    {
        var categoryExists = await _categoryService.UpdateCategoryAsync(id, newTitle);
        if (categoryExists is null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deletedCategory = await _categoryService.DeleteCategoryAsync(id);
        if (!deletedCategory)
        {
            return NotFound();
        }
        return NoContent();
    }
    
    
    
}