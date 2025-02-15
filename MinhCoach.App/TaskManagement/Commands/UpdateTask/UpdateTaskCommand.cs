using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Response;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.Commands.UpdateTask;

public record UpdateTaskCommand(
    Guid TaskId,
    string Title,
    string? Description,
    string? Priority,
    string? Status,
    DateTime StartTime,
    DateTime EndTime
    ) : IRequest<ErrorOr<ObjectResponse<Task>>>;