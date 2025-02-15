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
    DateTime EndTime
    ) : IRequest<ErrorOr<ObjectResponse<Task>>>;