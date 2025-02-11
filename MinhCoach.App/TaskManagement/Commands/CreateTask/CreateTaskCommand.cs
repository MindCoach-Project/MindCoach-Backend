using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Response;
using MinhCoach.App.TaskManagement.Common;

namespace MinhCoach.App.TaskManagement.Commands.CreateTask;

public record CreateTaskCommand(
    string Title,
    string? Description,
    string? Priority,
    DateTime StartTime,
    DateTime EndTime
    ) : IRequest<ErrorOr<ObjectResponse<CUDTaskResult>>>;