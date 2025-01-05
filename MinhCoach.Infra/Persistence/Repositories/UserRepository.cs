using MinhCoach.App.Common.Persistence;
using MinhCoach.Domain.Models;
using MinhCoach.Domain.User;

namespace MinhCoach.Infra.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    // private static readonly List<User> _users = new();
    private readonly MindCoachDbContext _dbContext;
    
    public UserRepository(MindCoachDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Add(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public User? GetUserByEmail(string email)
    {
        return _dbContext.Users.SingleOrDefault(u => u.Email == email);
    }
}