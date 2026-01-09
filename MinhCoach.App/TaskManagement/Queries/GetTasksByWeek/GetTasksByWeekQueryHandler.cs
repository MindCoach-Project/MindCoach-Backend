using MediatR;
using ErrorOr;
using AErrors = MinhCoach.App.Common.Errors.Errors;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Interfaces.Services;
using MinhCoach.App.Common.Interfaces.Persistence;
using MinhCoach.App.Common.Response;
using MinhCoach.App.TaskManagement.DomainModels;
using MinhCoach.Domain.User.ValueObjects;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.Queries.GetTasksByWeek;

public class GetTasksByWeekQueryHandler :
    IRequestHandler<GetTasksByWeekQuery, ErrorOr<ObjectResponse<List<UnifiedTaskDomainModel>>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public GetTasksByWeekQueryHandler(
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        IDateTimeProvider dateTimeProvider
        )
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task<ErrorOr<ObjectResponse<List<UnifiedTaskDomainModel>>>> Handle(
        GetTasksByWeekQuery query,
        CancellationToken cancellationToken)
    {   
        //get userid from token
        var userId = _tokenService.GetUserIdFromToken();
        if (userId == Guid.Empty)
        {
            return AErrors.Authentication.UserIdFromTokenNotFound;
        }
        
        //get task and subtask by week
        var (startOfWeek, endOfWeek) = _dateTimeProvider.GetWeekRange(query.Date);
        var tasks = await _unitOfWork.TaskRepository.GetTasksByWeekAsync(
            startOfWeek,
            endOfWeek,
            UserId.Create(userId));
        
        var unifiedTasks = tasks.SelectMany(t => t.SubTasks.Any()
                ? t.SubTasks.Select(s => 
                    UnifiedTaskDomainModel.FromSubTask(s))
                : new List<UnifiedTaskDomainModel>() { UnifiedTaskDomainModel.FromTask(t)})
            .OrderBy(t => t.TaskDetail.StartTime)
            .ToList();
      
      return new ObjectResponse<List<UnifiedTaskDomainModel>>(
          "Tasks retrieved successfully!",
          unifiedTasks
      );
    }
    
}