using MinhCoach.Domain.User;
using MinhCoach.Domain.User.ValueObjects;

namespace MinhCoach.App.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task Add(User user);
    Task<User?> GetUserByEmail(string email);
    
    Task<User?> GetUserById(UserId userId);

}