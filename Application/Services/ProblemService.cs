using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Mapster;

namespace Application.Services;

public class ProblemService :  IProblemService
{
    
    private readonly IProblemRepository _problemRepo;

    public ProblemService(IProblemRepository problemRepo)
    {
        _problemRepo = problemRepo;
    }

    public async Task<IEnumerable<ProblemResponse>> GetAllProblemsAsync()
    {
        var problems = await _problemRepo.GetAllAsync();
        return problems.Adapt<IEnumerable<ProblemResponse>>();
    }

    public async Task<ProblemResponse?> GetProblemByIdAsync(Guid id)
    {
        var problem = await _problemRepo.GetByIdAsync(id);
        return problem?.Adapt<ProblemResponse>();
    }

    public async Task<ProblemResponse?> GetProblemByTitleAsync(string title)
    {
        var problem = await _problemRepo.GetProblemByTitleAsync(title);
        return problem?.Adapt<ProblemResponse>();
    }

    public async Task<IEnumerable<ProblemResponse>> GetProblemsByDifficultyAsync(string difficulty)
    {
        var problems = await _problemRepo.GetProblemsByDifficultyAsync(difficulty);
        return problems.Adapt<IEnumerable<ProblemResponse>>();
    }

    public async Task<IEnumerable<ProblemResponse>> GetProblemsByCategoryAsync(Guid id)
    {
        var problems = await _problemRepo.GetProblemsByCategoryAsync(id);
        return  problems.Adapt<IEnumerable<ProblemResponse>>();
    }

    public async Task<IEnumerable<ProblemResponse>> GetProblemsByCategoryAsync(string title)
    {
        var problems = await _problemRepo.GetProblemsByCategoryAsync(title);
        return  problems.Adapt<IEnumerable<ProblemResponse>>();
    }

    public async Task<IEnumerable<ProblemResponse>> GetProblemsByUserAsync(Guid userId)
    {
        var problems = await _problemRepo.GetProblemsByUserAsync(userId);
        return problems.Adapt<IEnumerable<ProblemResponse>>();
    }

    public async Task<IEnumerable<ProblemResponse>> GetProblemsByUserAsync(string username)
    {
        var problems = await _problemRepo.GetProblemsByUserAsync(username);
        return problems.Adapt<IEnumerable<ProblemResponse>>();
    }

    public async Task<ProblemResponse?> CreateProblemAsync(ProblemRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<ProblemResponse?> UpdateProblemAsync(Guid id, ProblemRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteProblemAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AssignCategoryToProblemAsync(Guid problemId, Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UnassignCategoryFromProblemAsync(Guid problemId, Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AssignTestToProblemAsync(Guid problemId, Guid testId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UnAssignTestFromProblemAsync(Guid problemId, Guid testId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AssignTemplateToProblemAsync(Guid problemId, Guid templateId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UnAssignTemplateFromProblemAsync(Guid problemId, Guid templateId)
    {
        throw new NotImplementedException();
    }
}