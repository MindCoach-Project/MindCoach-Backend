using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Response;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.Queries.GetTaskById;

public record GetTaskByIdQuery(
    Guid TaskId
    ) : IRequest<ErrorOr<ObjectResponse<Task>>>;