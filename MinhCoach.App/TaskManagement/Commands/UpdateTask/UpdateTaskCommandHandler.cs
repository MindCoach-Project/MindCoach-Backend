using MediatR;
using ErrorOr;
using MinhCoach.Domain.Common.Errors;
using AErrors = MinhCoach.App.Common.Errors.Errors;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Persistence;
using MinhCoach.App.Common.Response;
using MinhCoach.Domain.Task.ValueObjects;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.Commands.UpdateTask;

public class UpdateTaskCommandHandler :
    IRequestHandler<UpdateTaskCommand, ErrorOr<ObjectResponse<Task>>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITokenService _tokenService;
    
    public UpdateTaskCommandHandler(
        ITaskRepository taskRepository,
        ITokenService tokenService)
    {
        _taskRepository = taskRepository;
        _tokenService = tokenService;
    }
    
    public async Task<ErrorOr<ObjectResponse<Task>>> Handle(
        UpdateTaskCommand command,
        CancellationToken cancellationToken)
    {   
        //get userid from token
        var userId = _tokenService.GetUserIdFromToken();
        if (userId == Guid.Empty)
        {
            return AErrors.Authentication.UserIdFromTokenNotFound;
        }
    
       //check task exist
       if (await  _taskRepository.FindByIdAsync(TaskId.Create(command.TaskId)) is not Task task)
           return Errors.Task.NotFound;
       
       //check access permission
       if (task.UserId.Value != userId)
           return Errors.Task.UnauthorizedAccess;
       
      //update task
      task.Update(
          command.Title,
          command.Description,
          command.Priority,
          command.Status,
          command.StartTime,
          command.EndTime);

      await _taskRepository.UpdateAsync(task);

      return new ObjectResponse<Task>(
          "Task updated! Your task is now in the list.",
          task
      );
    }

}