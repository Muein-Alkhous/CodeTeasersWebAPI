using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ProblemRepository : Repository<Problem>, IProblemRepository
{
    private readonly AppDbContext _context;

    public ProblemRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Problem?> GetProblemByTitleAsync(string title)
    {
        return await _context
            .Problems
            .Include(p => p.ProblemCategories)
            .FirstOrDefaultAsync(e => e.Title == title);
    }
    
}