using MinhCoach.Domain.User;

namespace MinhCoach.App.Common.Persistence;

public interface IUserRepository
{
    void Add(User user);
    User? GetUserByEmail(string email);
}