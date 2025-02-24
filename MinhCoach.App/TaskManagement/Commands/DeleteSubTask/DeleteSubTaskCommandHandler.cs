using MediatR;
using ErrorOr;
using MinhCoach.Domain.Common.Errors;
using AErrors = MinhCoach.App.Common.Errors.Errors;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Persistence;
using MinhCoach.App.Common.Response;
using MinhCoach.Domain.SubTask;
using MinhCoach.Domain.SubTask.ValueObjects;
using MinhCoach.Domain.Task.ValueObjects;
using Task = MinhCoach.Domain.Task.Task;

namespace MinhCoach.App.TaskManagement.Commands.DeleteSubTask;

public class DeleteSubTaskCommandHandler :
    IRequestHandler<DeleteSubTaskCommand, ErrorOr<ObjectResponse<SubTask>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    
    public DeleteSubTaskCommandHandler(
        IUnitOfWork unitOfWork,
        ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }
    
    public async Task<ErrorOr<ObjectResponse<SubTask>>> Handle(
        DeleteSubTaskCommand command,
        CancellationToken cancellationToken)
    {   
        //get userid from token
        var userId = _tokenService.GetUserIdFromToken();
        if (userId == Guid.Empty)
        {
            return AErrors.Authentication.UserIdFromTokenNotFound;
        }
        
       //check task exist
       if (await  _unitOfWork.TaskRepository
               .FindByIdAsync(TaskId.Create(command.TaskId)) is not Task task)
           return Errors.SubTask.NotFound;

       //check access permission
       if (task.UserId.Value != userId)
           return Errors.SubTask.UnauthorizedAccess;
       
       if (await _unitOfWork.SubTaskRepository
               .ValidateSubtaskMatchWithTask(
                   TaskId.Create(command.TaskId),
                   SubTaskId.Create(command.SubTaskId)) is not SubTask subTask)
       {
           return Errors.SubTask.NotFound;
       }
       
      //delete subTask
      subTask.SoftDelete();
      await _unitOfWork.SubTaskRepository.UpdateAsync(subTask);
      await _unitOfWork.SaveChangesAsync();
      
      return new ObjectResponse<SubTask>(
          "SubTask deleted successfully!.",
          subTask
      );
    }

}