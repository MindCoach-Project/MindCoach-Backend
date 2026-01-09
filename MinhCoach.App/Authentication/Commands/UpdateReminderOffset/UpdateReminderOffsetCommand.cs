using MediatR;
using ErrorOr;
using MinhCoach.App.Authentication.Common;
using MinhCoach.App.Common.Response;
using MinhCoach.Domain.User;

namespace MinhCoach.App.Authentication.Commands.UpdateReminderOffset;

public record UpdateReminderOffsetCommand(
    int? ReminderOffset
    ) : IRequest<ErrorOr<ObjectResponse<User>>>;