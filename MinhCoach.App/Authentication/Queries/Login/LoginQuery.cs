using MediatR;
using ErrorOr;
using MinhCoach.App.Authentication.Common;

namespace MinhCoach.App.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;