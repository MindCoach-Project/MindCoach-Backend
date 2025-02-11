using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Errors;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Persistence;
using MinhCoach.App.Common.Response;
using MinhCoach.App.TaskManagement.Common;
using Task = MinhCoach.Domain.Task;

namespace MinhCoach.App.TaskManagement.Commands.CreateTask;

public class CreateTaskCommandHandler :
    IRequestHandler<CreateTaskCommand, ErrorOr<ObjectResponse<CUDTaskResult>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITokenService _tokenService;
    
    public CreateTaskCommandHandler(
        ITaskRepository taskRepository,
        ITokenService tokenService)
    {
        _taskRepository = taskRepository;
        _tokenService = tokenService;
    }
    
    public async Task<ErrorOr<ObjectResponse<CUDTaskResult>>> Handle(
        CreateTaskCommand command,
        CancellationToken cancellationToken)
    {   
        
       //get userid from token
       var userId = _tokenService.GetUserIdFromToken();
       if (userId == Guid.Empty)
       {
           return Errors.Authentication.UserIdFromTokenNotFound;
       }
       
      //create task
      var task = Task.Task.Create(
          command.Title,
          command.Description,
          command.Priority,
          command.StartTime,
          command.EndTime,
          userId);
        
      _taskRepository.Add(task);
      
      return new ObjectResponse<CUDTaskResult>(
          "Task created! Your task is now in the list.",
          new CUDTaskResult(task)
      );
    }

}