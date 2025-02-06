using MediatR;
using ErrorOr;
using MinhCoach.App.Authentication.Common;
using MinhCoach.App.Common.Response;

namespace MinhCoach.App.Authentication.Commands.Register;

public record RegisterCommand(
    string Username,
    string Email,
    string Password,
    string ConfirmPassword
    ) : IRequest<ErrorOr<ObjectResponse<AuthenticationResult>>>;