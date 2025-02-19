using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Errors;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Persistence;
using MinhCoach.App.Common.Response;
using MinhCoach.Domain.SubTask;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.Commands.CreateTask;

public class CreateTaskCommandHandler :
    IRequestHandler<CreateTaskCommand, ErrorOr<ObjectResponse<Task>>>
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
    
    public async Task<ErrorOr<ObjectResponse<Task>>> Handle(
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
      var task = Task.Create(
          command.Title,
          command.Description,
          command.Priority,
          command.StartTime,
          command.EndTime,
          userId,
          command.SubTasks?.ConvertAll(s => SubTask.Create(
              s.Title,
              s.Description,
              s.StartTime,
              s.EndTime)) ?? new List<SubTask>());
      
      await _taskRepository.AddAsync(task);

      return new ObjectResponse<Task>(
          "Task created! Your task is now in the list.",
          task
      );
    }

}