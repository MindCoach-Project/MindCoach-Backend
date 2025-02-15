using MediatR;
using ErrorOr;
using MinhCoach.Domain.Common.Errors;
using AErrors = MinhCoach.App.Common.Errors.Errors;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Persistence;
using MinhCoach.App.Common.Response;
using Task = MinhCoach.Domain.Task.Task;
using MinhCoach.Domain.Task.ValueObjects;


namespace MinhCoach.App.TaskManagement.Queries.GetTaskById;

public class GetTaskByIdQueryHandler :
    IRequestHandler<GetTaskByIdQuery, ErrorOr<ObjectResponse<Task>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITokenService _tokenService;
    
    public GetTaskByIdQueryHandler(
        ITaskRepository taskRepository,
        ITokenService tokenService)
    {
        _taskRepository = taskRepository;
        _tokenService = tokenService;
    }
    
    public async Task<ErrorOr<ObjectResponse<Task>>> Handle(
        GetTaskByIdQuery query,
        CancellationToken cancellationToken)
    {   
        //get userid from token
        var userId = _tokenService.GetUserIdFromToken();
        if (userId == Guid.Empty)
        {
            return AErrors.Authentication.UserIdFromTokenNotFound;
        }
    
       //check task exist
       if (await  _taskRepository.FindByIdAsync(TaskId.Create(query.TaskId)) is not Task task)
           return Errors.Task.NotFound;
       
       //check access permission
       if (task.UserId.Value != userId)
           return Errors.Task.UnauthorizedAccess;
      
      return new ObjectResponse<Task>(
          "Task retrieved successfully!",
          task
      );
    }

}