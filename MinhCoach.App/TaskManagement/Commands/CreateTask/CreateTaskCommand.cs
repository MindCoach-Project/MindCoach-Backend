using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Response;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.Commands.CreateTask;

public record CreateTaskCommand(
    string Title,
    string? Description,
    string? Priority,
    DateTime StartTime,
    DateTime EndTime,
    List<SubTaskCommand>? SubTasks = null
    ) : IRequest<ErrorOr<ObjectResponse<Task>>>;
    
    public record SubTaskCommand(
        string Title,
        string? Description,
        DateTime StartTime,
        DateTime EndTime
        );