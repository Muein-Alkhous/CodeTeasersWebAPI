using Domain.Entities;

namespace Domain.Interfaces;

public interface ISubmissionRepository : IRepository<Submission>
{
    Task<IEnumerable<Problem>> GetProblemsByUserAsync(Guid userId);
    Task<IEnumerable<Category>> GetUsersByProblemAsync(Guid problemId);
}