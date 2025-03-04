using Microsoft.EntityFrameworkCore;
using MinhCoach.App.Common.Interfaces.Persistence;
using MinhCoach.Domain.User;
using MinhCoach.Domain.User.ValueObjects;

namespace MinhCoach.Infra.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    // private static readonly List<User> _users = new();
    private readonly MindCoachDbContext _dbContext;
    
    public UserRepository(MindCoachDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<User>? GetUserByEmail(string email)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserById(UserId userId)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);
    }
}