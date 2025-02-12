using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Response;
using MinhCoach.App.TaskManagement.Common;

namespace MinhCoach.App.TaskManagement.Commands.UpdateTask;

public record UpdateTaskCommand(
    Guid TaskId,
    string Title,
    string? Description,
    string? Priority,
    DateTime StartTime,
    DateTime EndTime
    ) : IRequest<ErrorOr<ObjectResponse<CUDTaskResult>>>;