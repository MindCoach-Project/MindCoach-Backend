using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Response;
using MinhCoach.App.TaskManagement.DomainModels;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.Queries.GetTasksByWeek;

public record GetTasksByWeekQuery(
    DateTime Date
    ) : IRequest<ErrorOr<ObjectResponse<List<UnifiedTaskDomainModel>>>>;