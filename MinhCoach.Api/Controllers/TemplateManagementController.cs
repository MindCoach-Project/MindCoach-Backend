using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhCoach.App.Common.Response;
using MinhCoach.App.TemplateManagement.Commands.GenerateTasksFromTemplate;
using MinhCoach.App.TemplateManagement.Queries.GetTemplates;
using MinhCoach.Contracts.TaskManagement;
using MinhCoach.Contracts.TemplateManagement;

namespace MinhCoach.Api.Controllers;

[Route("template-management/templates")]
public class TemplateManagementController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public TemplateManagementController(
        IMediator mediator,
        IMapper mapper
    )
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetTemplates([FromQuery] string? templateType = null)
    {
        var query = new GetTemplatesQuery(templateType);
        var taskResult = await _mediator.Send(query);
        return taskResult.Match(
            response => Ok(_mapper.Map<ApiResponse<List<TemplateResponse>>>(response)),
            errors=> Problem(errors));
    }
    
    [HttpPost("create-task-from-template/{templateId}")]
    public async Task<IActionResult> GenerateTasksFromTemplates(Guid templateId)
    {
        var command = new GenerateTasksFromTemplateCommand(templateId);
        var taskResult = await _mediator.Send(command);
        return taskResult.Match(
            response => Ok(_mapper.Map<ApiResponse<List<CUDTaskResponse>>>(response)),
            errors=> Problem(errors));
    }
}