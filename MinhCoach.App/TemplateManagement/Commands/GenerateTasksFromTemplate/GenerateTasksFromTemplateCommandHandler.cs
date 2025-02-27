using MediatR;
using ErrorOr;
using AErrors = MinhCoach.App.Common.Errors.Errors;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Interfaces.Persistence;
using MinhCoach.App.Common.Interfaces.Services;
using MinhCoach.App.Common.Response;
using MinhCoach.Domain.Common.Errors;
using MinhCoach.Domain.Template;
using MinhCoach.Domain.Template.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;
using Task = MinhCoach.Domain.Task.Task;
namespace MinhCoach.App.TemplateManagement.Commands.GenerateTasksFromTemplate;

public class GenerateTasksFromTemplateCommandHandler :
    IRequestHandler<GenerateTasksFromTemplateCommand, ErrorOr<ObjectResponse<List<Task>>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly ITaskService _taskService;

    
    public GenerateTasksFromTemplateCommandHandler(
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        ITaskService taskService
            )
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _taskService = taskService;
    }
    
    public async Task<ErrorOr<ObjectResponse<List<Task>>>> Handle(
        GenerateTasksFromTemplateCommand command,
        CancellationToken cancellationToken)
    {   
        
       //get userid from token
       var userId = _tokenService.GetUserIdFromToken();
       if (userId == Guid.Empty)
       {
           return AErrors.Authentication.UserIdFromTokenNotFound;
       }

       //check template is exits
       if (await _unitOfWork.TemplateRepository.FindByIdAsync(
               TemplateId.Create(command.TemplateId)) is not Template template)
           return Errors.Template.NotFound;
       
       //check if template belongs to user 
       if(template.IsPrivateTemplate && 
          template.UserId.Value != userId)
           return Errors.Template.NotFound;
      
      //synchronize generate task
      var tasks = await _taskService.GenerateAndSaveTasks(
          template,
          UserId.Create(userId));
      
      return new ObjectResponse<List<Task>>(
          "Tasks generated successfully!",
          tasks
      );
    }

}