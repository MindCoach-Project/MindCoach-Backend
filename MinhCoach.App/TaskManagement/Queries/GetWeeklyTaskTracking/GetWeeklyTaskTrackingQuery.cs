using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Response;
using MinhCoach.App.TaskManagement.Common;

namespace MinhCoach.App.TaskManagement.Queries.GetWeeklyTaskTracking;

public record GetWeeklyTaskTrackingQuery(
    ) : IRequest<ErrorOr<ObjectResponse<List<GetDailyTaskTrackingResult>>>>;