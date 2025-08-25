using Application.DTOs;
using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class ProblemService :  IProblemService
{
    
    private readonly IProblemRepository _problemRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IDescriptionRepository _descriptionRepo;

    public ProblemService(IProblemRepository problemRepo, ICategoryRepository categoryRepo, IDescriptionRepository descriptionRepo)
    {
        _problemRepo = problemRepo;
        _categoryRepo = categoryRepo;
        _descriptionRepo = descriptionRepo;
    }

    public async Task<IEnumerable<ProblemResponse>> GetAllProblemsAsync()
    {
        var problems = await _problemRepo.GetAllProblemsAsync();
        return problems.Adapt<IEnumerable<ProblemResponse>>();
    }

    public async Task<ProblemResponse?> GetProblemByIdAsync(Guid id)
    {
        var problem = await _problemRepo.GetProblemByIdAsync(id);
        if (problem == null)
            throw new NotFoundException($"Problem with Id:{id} not found");
        return problem?.Adapt<ProblemResponse>();
    }

    public async Task<ProblemResponse?> GetProblemByTitleAsync(string title)
    {
        var problem = await _problemRepo.GetProblemByTitleAsync(title);
        if (problem == null)
            throw new NotFoundException($"Problem with Title:{title} not found");
        return problem?.Adapt<ProblemResponse>();
    }

    public async Task<IEnumerable<ProblemResponse>> GetProblemsByDifficultyAsync(string difficulty)
    {
        var problems = await _problemRepo.GetProblemsByDifficultyAsync(difficulty);
        return problems.Adapt<IEnumerable<ProblemResponse>>();
    }

    public async Task<IEnumerable<ProblemResponse>> GetProblemsByCategoryAsync(Guid id)
    {
        if (!await _categoryRepo.ExistsAsync(id))
            throw new NotFoundException($"Category with Id:{id} not found");
        var problems = await _problemRepo.GetProblemsByCategoryAsync(id);
        return  problems.Adapt<IEnumerable<ProblemResponse>>();
    }

    public async Task<IEnumerable<ProblemResponse>> GetProblemsByCategoryAsync(string title)
    {
        if(!await _categoryRepo.CategoryExistsByTitleAsync(title))
            throw new NotFoundException($"Category with Title:{title} not found");
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
       
        if (!await _problemRepo.ExistsByTitleAsync(request.Title))
        {
            throw new ConflictException($"Problem with title:{request.Title} already exists");
        }

        foreach (var categoryId in request.CategoriesId)
        {
            if(!await _categoryRepo.ExistsAsync(categoryId))
                throw new NotFoundException($"Category with id:{categoryId} does not exist");
        }

        var newProblem = new Problem
        {
            Title = request.Title,
            Difficulty = request.Difficulty,
        };

        await _problemRepo.AddAsync(newProblem);
        
        await _problemRepo.AssignCategoriesToProblemAsync(newProblem.Id, request.CategoriesId);
        
        var problemToReturn = await _problemRepo.GetProblemByIdAsync(newProblem.Id);
        
        return problemToReturn.Adapt<ProblemResponse>();
    }

    public async Task<ProblemResponse?> UpdateProblemAsync(Guid id, ProblemRequest request)
    {
        var oldProblem = await _problemRepo.GetProblemByIdAsync(id);
        if (oldProblem == null)
        {
            throw new ConflictException($"Problem with Id:{id} does not exists");
        }

        if (oldProblem.Title == request.Title)
        {
            throw new ConflictException($"The problem title is already set to '{request.Title}'. No update was made.");
        }

        if (!await _problemRepo.ExistsByTitleAsync(request.Title))
        {
            throw new ConflictException($"Problem with Title:{request.Title} already exists");
        }

        foreach (var categoryId in request.CategoriesId)
        {
            if(!await _categoryRepo.ExistsAsync(categoryId))
                throw new NotFoundException($"Category with id:{categoryId} does not exist");
        }
        
        oldProblem.Title = request.Title;
        oldProblem.Difficulty = request.Difficulty;
        
        _problemRepo.Update(oldProblem);
        
        return oldProblem.Adapt<ProblemResponse>();
    }

    public async Task<bool> DeleteProblemAsync(Guid id)
    {
        var problem = await _problemRepo.GetProblemByIdAsync(id);

        if (problem is null)
        {
            return false;
        }

        _problemRepo.Delete(problem);
        
        return true;
    }

    public async Task<DescriptionResponse?> GetDescriptionByIdAsync(Guid problemId)
    {
        var description = await _descriptionRepo.GetDescriptionByProblemIdAsync(problemId);
        if (description is null)
            throw new NotFoundException($"Description with Id:{problemId} does not exist");
        return description.Adapt<DescriptionResponse>();
    }

    public async Task<bool> AssignCategoriesToProblemAsync(Guid problemId, IEnumerable<Guid> categoryIds)
    {
        var problem = await _problemRepo.GetProblemByIdAsync(problemId);
        if (problem is null)  return false;
        await _problemRepo.AssignCategoriesToProblemAsync(problemId, categoryIds);
        return true;

    }


    public async Task<DescriptionResponse> AddOrUpdateDescriptionAsync(Guid problemId, IFormFile file)
    {
        var problem = await _problemRepo.GetProblemByIdAsync(problemId);
        if (problem is null)
            throw new NotFoundException($"Problem with Id:{problemId} does not exist");
        if (file == null || file.Length == 0)
            throw new Exception("No file uploaded.");
        
        var description = new Description();
        description.Id = problemId;
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            description.Data = memoryStream.ToArray();
        }
        await _descriptionRepo.AddAsync(description);
        return description.Adapt<DescriptionResponse>();
        
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