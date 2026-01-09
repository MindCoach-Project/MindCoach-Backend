using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Interfaces.Persistence;
using MinhCoach.App.Common.Response;
using MinhCoach.App.TaskManagement.DomainModels;
using MinhCoach.App.TemplateManagement.Common;
using MinhCoach.Domain.User.ValueObjects;

namespace MinhCoach.App.TemplateManagement.Queries.GetTemplates;

public class GetTemplatesQueryHandler :
    IRequestHandler<GetTemplatesQuery, ErrorOr<ObjectResponse<List<GetTemplatesResult>>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    
    public GetTemplatesQueryHandler(
        IUnitOfWork unitOfWork,
        ITokenService tokenService
    )
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }
    
    public async Task<ErrorOr<ObjectResponse<List<GetTemplatesResult>>>> Handle(
        GetTemplatesQuery query,
        CancellationToken cancellationToken)
    {   
      //get system tempates
      var userId = _tokenService.GetUserIdFromToken();
   
      var templates = await _unitOfWork.TemplateRepository.GetTemplates(
          userId != Guid.Empty ? UserId.Create(userId) : null,
          query.templateType);
      
      //get task and subtask in templates
      var getTemplatesResult = new List<GetTemplatesResult>();
      foreach (var template in templates)
      {
          var tasksInTemplate =   await _unitOfWork.TaskRepository.GetTaskByTemplateId(
              template.Id,
              10);
          
          var unifiedTasks = tasksInTemplate.SelectMany(t => t.SubTasks.Any()
                  ? t.SubTasks.Select(s => 
                      UnifiedTaskDomainModel.FromSubTask(s))
                  : new List<UnifiedTaskDomainModel>() { UnifiedTaskDomainModel.FromTask(t)})
              .OrderBy(t => t.TaskDetail.StartTime)
              .Take(10)
              .ToList();
            
          getTemplatesResult.Add(new GetTemplatesResult(
              template.Id,
              template.Name,
              template.Description,
              template.IsPrivateTemplate,
              template.Timestamps,
              template.UserId,
              unifiedTasks
              ));
      }
      
      return new ObjectResponse<List<GetTemplatesResult>>(
          "Templates retrieved successfully!",
          getTemplatesResult
      );
    }
}