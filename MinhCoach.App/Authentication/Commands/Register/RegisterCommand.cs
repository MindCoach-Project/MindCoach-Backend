// using MediatR;
// using ErrorOr;
// using MinhCoach.App.Authentication.Common;
//
// namespace MinhCoach.App.Authentication.Commands.Register;
//
// public record RegisterCommand(
//     string FirstName,
//     string LastName,
//     string Email,
//     string Password
//     ) : IRequest<ErrorOr<AuthenticationResult>>;