using Application.DTOs.Response;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Mapster;

namespace Application.Services;

public class DescriptionService : IDescriptionService
{
    private readonly IDescriptionRepository _descriptionRepository;
    private readonly IProblemRepository _problemRepository;

    public DescriptionService(IDescriptionRepository descriptionRepository, IProblemRepository problemRepository)
    {
        _descriptionRepository = descriptionRepository;
        _problemRepository = problemRepository;
    }


    public async Task<DescriptionResponse?> GetDescriptionByProblemIdAsync(Guid problemId)
    {
        var description = await _descriptionRepository.GetDescriptionByProblemIdAsync(problemId);
        if (description == null) 
            throw new NotFoundException($"Description with Id: {problemId} not found");
        
        return description.Adapt<DescriptionResponse>();

    }

    public async Task<DescriptionResponse> CreateOrUpdateDescriptionAsync(Guid problemId, string markdownContent)
    {
        var problem = await _problemRepository.GetProblemByIdAsync(problemId);
        if (problem == null) throw new Exception("Problem not found.");
        
        // Check if description already exists
        var existingDescription = await _descriptionRepository.GetDescriptionByProblemIdAsync(problemId);

        if (existingDescription != null)
        {
            existingDescription.Content = markdownContent;
            _descriptionRepository.Update(existingDescription);
            return existingDescription.Adapt<DescriptionResponse>();
        }

        var newDescription = new Description
        {
            Id = problem.Id,
            Content = markdownContent,
            Problem = problem
        };

        await _descriptionRepository.AddAsync(newDescription);
        return newDescription.Adapt<DescriptionResponse>();
    }
}