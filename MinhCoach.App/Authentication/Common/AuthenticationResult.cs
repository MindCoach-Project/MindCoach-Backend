using MinhCoach.Domain.User;

namespace MinhCoach.App.Authentication.Common;

public record AuthenticationResult
(
    User User,
    string Token
);
