using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Response;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.Queries.GetUpcomingTasks;

public record GetUpcomingTasksQuery(
    ) : IRequest<ErrorOr<ObjectResponse<List<Task>>>>;