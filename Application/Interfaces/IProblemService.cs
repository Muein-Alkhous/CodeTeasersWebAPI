using Application.DTOs.Request;
using Application.DTOs.Response;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IProblemService
{
    Task<IEnumerable<ProblemResponse>> GetAllProblemsAsync();
    Task<ProblemResponse?> GetProblemByIdAsync(Guid id);
    Task<ProblemResponse?> GetProblemByTitleAsync(string title);
    Task<IEnumerable<ProblemResponse>> GetProblemsByDifficultyAsync(string difficulty);
    Task<IEnumerable<ProblemResponse>> GetProblemsByCategoryAsync(Guid id);
    Task<IEnumerable<ProblemResponse>> GetProblemsByCategoryAsync(string title);
    Task<IEnumerable<ProblemResponse>> GetProblemsByUserAsync(Guid userId);
    Task<IEnumerable<ProblemResponse>> GetProblemsByUserAsync(string username);

    Task<ProblemResponse?> CreateProblemAsync(ProblemRequest request);
    Task<ProblemResponse?> UpdateProblemAsync(Guid id, ProblemRequest request);
    Task<bool> DeleteProblemAsync(Guid id);
    Task<DescriptionResponse> AddOrUpdateDescriptionAsync(Guid problemId, IFormFile file);
    Task <DescriptionResponse?> GetDescriptionByIdAsync(Guid problemId);
    
    Task<bool> AssignCategoriesToProblemAsync(Guid problemId, IEnumerable<Guid> categoryIds);

    Task<bool> AssignTestToProblemAsync(Guid problemId, Guid testId);
    Task<bool> UnAssignTestFromProblemAsync(Guid problemId, Guid testId);
    
    Task<bool> AssignTemplateToProblemAsync(Guid problemId, Guid templateId);
    Task<bool> UnAssignTemplateFromProblemAsync(Guid problemId, Guid templateId);
    
}