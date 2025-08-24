using Application.DTOs.Response;
using Domain.Entities;

namespace Application.Interfaces;

public interface IDescriptionService
{
    Task<DescriptionResponse?> GetDescriptionByProblemIdAsync(Guid problemId);
    Task<DescriptionResponse> CreateOrUpdateDescriptionAsync(Guid problemId, string markdownContent);
}