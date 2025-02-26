using MinhCoach.Domain.User;

namespace MinhCoach.App.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task Add(User user);
    Task<User?> GetUserByEmail(string email);
}