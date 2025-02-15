using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Response;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.Commands.DeleteTask;

public record DeleteTaskCommand(
    Guid TaskId
    ) : IRequest<ErrorOr<ObjectResponse<Task>>>;