using MinhCoach.Domain.Models;
using MinhCoach.Domain.User;

namespace MinhCoach.App.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}