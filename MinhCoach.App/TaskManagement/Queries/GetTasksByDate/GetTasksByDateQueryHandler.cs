using MediatR;
using ErrorOr;
using AErrors = MinhCoach.App.Common.Errors.Errors;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Persistence;
using MinhCoach.App.Common.Response;
using MinhCoach.Domain.Common.Enums;
using MinhCoach.Domain.Common.Utilities;
using Task = MinhCoach.Domain.Task.Task;
using MinhCoach.Domain.User.ValueObjects;

namespace MinhCoach.App.TaskManagement.Queries.GetTasksByDate;

public class GetTasksByDateQueryHandler :
    IRequestHandler<GetTasksByDateQuery, ErrorOr<ObjectResponse<List<Task>>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITokenService _tokenService;
    
    public GetTasksByDateQueryHandler(
        ITaskRepository taskRepository,
        ITokenService tokenService)
    {
        _taskRepository = taskRepository;
        _tokenService = tokenService;
    }
    
    public async Task<ErrorOr<ObjectResponse<List<Task>>>> Handle(
        GetTasksByDateQuery query,
        CancellationToken cancellationToken)
    {   
        //get userid from token
        var userId = _tokenService.GetUserIdFromToken();
        if (userId == Guid.Empty)
        {
            return AErrors.Authentication.UserIdFromTokenNotFound;
        }
        
        //get task by date and status
        var statusEnum = EnumUtilities.ParseEnum<TaskStatuses>(query.Status);
        
        var tasks = await _taskRepository.GetTasksByDateAsync(
            query.Date, 
            statusEnum, 
            UserId.Create(userId));
      
      return new ObjectResponse<List<Task>>(
          "Tasks retrieved successfully!",
          tasks
      );
    }

}