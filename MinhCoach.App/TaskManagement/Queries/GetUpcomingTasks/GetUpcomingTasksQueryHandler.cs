using MediatR;
using ErrorOr;
using AErrors = MinhCoach.App.Common.Errors.Errors;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Interfaces.Services;
using MinhCoach.App.Common.Interfaces.Persistence;
using MinhCoach.App.Common.Response;
using MinhCoach.Domain.User.ValueObjects;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.Queries.GetUpcomingTasks;

public class GetUpcomingTasksQueryHandler :
    IRequestHandler<GetUpcomingTasksQuery, ErrorOr<ObjectResponse<List<Task>>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public GetUpcomingTasksQueryHandler(
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        IDateTimeProvider dateTimeProvider
        )
    {
        _unitOfWork = unitOfWork;
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
        var now = _dateTimeProvider.UtcNow;
        
        //get upcoming task today
        var tasks = await _unitOfWork.TaskRepository.GetUpcomingTasksTodayAsync(
            now,
            UserId.Create(userId));
      
      return new ObjectResponse<List<Task>>(
          "Tasks retrieved successfully!",
          tasks
      );
    }

}