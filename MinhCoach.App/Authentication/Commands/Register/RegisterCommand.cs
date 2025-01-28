using MediatR;
using ErrorOr;
using MinhCoach.App.Authentication.Common;

namespace MinhCoach.App.Authentication.Commands.Register;

public record RegisterCommand(
    string Username,
    string Email,
    string Password,
    string ConfirmPassword
    ) : IRequest<ErrorOr<AuthenticationResult>>;