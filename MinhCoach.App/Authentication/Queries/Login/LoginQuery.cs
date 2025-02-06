using MediatR;
using ErrorOr;
using MinhCoach.App.Authentication.Common;
using MinhCoach.App.Common.Response;

namespace MinhCoach.App.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
    ) : IRequest<ErrorOr<ObjectResponse<AuthenticationResult>>>;