using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Response;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.Queries.GetTasksByDate;

public record GetTasksByDateQuery(
    DateTime Date,
    string Status
    ) : IRequest<ErrorOr<ObjectResponse<List<Task>>>>;