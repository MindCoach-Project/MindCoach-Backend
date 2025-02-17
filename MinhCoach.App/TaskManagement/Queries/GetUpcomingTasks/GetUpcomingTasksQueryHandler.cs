using MediatR;
using ErrorOr;
using AErrors = MinhCoach.App.Common.Errors.Errors;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Interfaces.Services;
using MinhCoach.App.Common.Persistence;
using MinhCoach.App.Common.Response;
using Task = MinhCoach.Domain.Task.Task;
using MinhCoach.Domain.User.ValueObjects;

namespace MinhCoach.App.TaskManagement.Queries.GetUpcomingTasks;

public class GetUpcomingTasksQueryHandler :
    IRequestHandler<GetUpcomingTasksQuery, ErrorOr<ObjectResponse<List<Task>>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITokenService _tokenService;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public GetUpcomingTasksQueryHandler(
        ITaskRepository taskRepository,
        ITokenService tokenService,
        IDateTimeProvider dateTimeProvider
        )
    {
        _taskRepository = taskRepository;
        _tokenService = tokenService;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task<ErrorOr<ObjectResponse<List<Task>>>> Handle(
        GetUpcomingTasksQuery query,
        CancellationToken cancellationToken)
    {   
        //get userid from token
        var userId = _tokenService.GetUserIdFromToken();
        if (userId == Guid.Empty)
        {
            return AErrors.Authentication.UserIdFromTokenNotFound;
        }
        
        //get current time
        var now = _dateTimeProvider.Now;
        
        //get upcoming task today
        var tasks = await _taskRepository.GetUpcomingTasksTodayAsync(
            now,
            UserId.Create(userId));
      
      return new ObjectResponse<List<Task>>(
          "Tasks retrieved successfully!",
          tasks
      );
    }

}