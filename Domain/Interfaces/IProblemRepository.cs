using Domain.Entities;

namespace Domain.Interfaces;

public interface IProblemRepository : IRepository<Problem>
{
    Task<Problem?> GetProblemByTitleAsync(string title);
}