using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class DescriptionRepository : Repository<Description>, IDescriptionRepository
{
    private readonly AppDbContext _context;

    public DescriptionRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Description?> GetDescriptionByProblemIdAsync(Guid problemId)
    {
        return await _context.Descriptions
            .FirstOrDefaultAsync(d => d.Id == problemId);
    }
}