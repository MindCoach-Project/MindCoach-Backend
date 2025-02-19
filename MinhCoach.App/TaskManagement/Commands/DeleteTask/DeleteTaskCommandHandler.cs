using MediatR;
using ErrorOr;
using MinhCoach.Domain.Common.Errors;
using AErrors = MinhCoach.App.Common.Errors.Errors;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Persistence;
using MinhCoach.App.Common.Response;
using MinhCoach.Domain.Task.ValueObjects;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.Commands.DeleteTask;

public class DeleteTaskCommandHandler :
    IRequestHandler<DeleteTaskCommand, ErrorOr<ObjectResponse<Task>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    
    public DeleteTaskCommandHandler(
        IUnitOfWork unitOfWork,
        ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }
    
    public async Task<ErrorOr<ObjectResponse<Task>>> Handle(
        DeleteTaskCommand command,
        CancellationToken cancellationToken)
    {   
        //get userid from token
        var userId = _tokenService.GetUserIdFromToken();
        if (userId == Guid.Empty)
        {
            return AErrors.Authentication.UserIdFromTokenNotFound;
        }
        Console.Write(command.TaskId);
       //check task exist
       if (await  _unitOfWork.TaskRepository.FindByIdAsync(TaskId.Create(command.TaskId)) is not Task task)
           return Errors.Task.NotFound;
       
       //check access permission
       if (task.UserId.Value != userId)
           return Errors.Task.UnauthorizedAccess;
       
       
      //delete task
      task.SoftDelete();
      await _unitOfWork.TaskRepository.UpdateAsync(task);
      
      return new ObjectResponse<Task>(
          "Task deleted!.",
          task
      );
    }

}