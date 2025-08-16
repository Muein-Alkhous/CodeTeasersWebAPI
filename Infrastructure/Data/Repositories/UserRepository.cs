using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    
    private readonly AppDbContext _context;
    
    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context
            .Users
            .Include(u => u.UserStatus)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<IEnumerable<User>> GetAllUsersWithStatusAsync()
    {
        return await _context
            .Users
            .Include(u => u.UserStatus)
            .ToListAsync();
    }
}