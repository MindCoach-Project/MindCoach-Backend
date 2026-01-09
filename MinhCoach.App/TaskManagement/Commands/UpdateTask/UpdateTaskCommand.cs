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
    DateTime EndTime,
    List<SubTaskCommand>? SubTasks = null
    ) : IRequest<ErrorOr<ObjectResponse<Task>>>;
    
public record SubTaskCommand(
    Guid Id,
    string Title,
    string? Description,
    string? Status,
    DateTime StartTime,
    DateTime EndTime
);