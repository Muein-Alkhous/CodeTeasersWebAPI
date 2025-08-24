using Application.DTOs;
using Application.DTOs.Request;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProblemController : ControllerBase
{
    private readonly IProblemService _problemService;

    public ProblemController(IProblemService problemService)
    {
        _problemService = problemService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllProblems(
        [FromQuery] string? difficulty, 
        [FromQuery] Guid? categoryId)
    {
        var problems = await _problemService.GetAllProblemsAsync(difficulty, categoryId);
        return Ok(problems);
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var problem =  await _problemService.GetProblemByIdAsync(id);
        return Ok(problem);
    }
    
    [HttpGet("by-title/{title}")]
    public async Task<IActionResult> Get(string title)
    {
        var problem =  await _problemService.GetProblemByTitleAsync(title);
        return Ok(problem);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProblemRequest problemRequest)
    {
        var problem = await _problemService.CreateProblemAsync(problemRequest);
        return Created("", problem);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] ProblemRequest problemRequest)
    {
        await _problemService.UpdateProblemAsync(id, problemRequest);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _problemService.DeleteProblemAsync(id);
        return NoContent();
    }
    
}