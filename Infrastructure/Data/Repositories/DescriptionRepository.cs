using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class DescriptionRepository(AppDbContext context) : Repository<Description>(context), IDescriptionRepository
{
    private readonly AppDbContext _context = context;
    
    public async Task<Description?> GetDescriptionByProblemIdAsync(Guid problemId)
    {
        return await _context.Descriptions
            .Include(d => d.Problem) // Problem navigation property
            .FirstOrDefaultAsync(d => d.Id == problemId);
    }
    
    
}