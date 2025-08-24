using Domain.Entities;

namespace Domain.Interfaces;

public interface IDescriptionRepository : IRepository<Description>
{
    Task<Description?> GetDescriptionByProblemIdAsync(Guid problemId);
}