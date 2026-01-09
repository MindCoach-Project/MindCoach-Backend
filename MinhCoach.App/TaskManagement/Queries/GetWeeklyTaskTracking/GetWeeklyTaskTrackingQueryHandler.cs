using MediatR;
using ErrorOr;
using AErrors = MinhCoach.App.Common.Errors.Errors;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Interfaces.Services;
using MinhCoach.App.Common.Interfaces.Persistence;
using MinhCoach.App.Common.Response;
using MinhCoach.App.TaskManagement.Common;
using MinhCoach.Domain.User.ValueObjects;

namespace MinhCoach.App.TaskManagement.Queries.GetWeeklyTaskTracking;

public class GetWeeklyTaskTrackingQueryHandler :
    IRequestHandler<GetWeeklyTaskTrackingQuery, ErrorOr<ObjectResponse<List<GetDailyTaskTrackingResult>>>>
{
    private readonly ITokenService _tokenService;
    private readonly ITaskService _taskService;
    
    public GetWeeklyTaskTrackingQueryHandler(
        ITokenService tokenService,
        ITaskService taskService
        )
    {
        _tokenService = tokenService;
        _taskService = taskService;
    }
    
    public async Task<ErrorOr<ObjectResponse<List<GetDailyTaskTrackingResult>>>> Handle(
        GetWeeklyTaskTrackingQuery query,
        CancellationToken cancellationToken)
    {   
        //get userid from token
        var userId = _tokenService.GetUserIdFromToken();
        if (userId == Guid.Empty)
        {
            return AErrors.Authentication.UserIdFromTokenNotFound;
        }
        
        //get weekly task tracking
        var result = await _taskService.GetTasksTrackingByWeekAsync(
            UserId.Create(userId)
            );
        
        return new ObjectResponse<List<GetDailyTaskTrackingResult>>(
            "Weekly task tracking retrieved successfully!",
                result
        );
    }
}