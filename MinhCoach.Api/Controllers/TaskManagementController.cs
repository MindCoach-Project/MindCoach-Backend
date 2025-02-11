using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhCoach.App.Common.Response;
using MinhCoach.App.TaskManagement.Commands.CreateTask;
using MinhCoach.Contracts.TaskManagement;

namespace MinhCoach.Api.Controllers;
[Route("tasks")]
[Authorize]
public class TaskManagementController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public TaskManagementController(
        IMediator mediator,
        IMapper mapper
        )
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskRequest req)
    {
        var command = _mapper.Map<CreateTaskCommand>(req);
        var authResult = await _mediator.Send(command);
        return authResult.Match(
            response =>  Created("url-to-new-resource", _mapper.Map<ApiResponse<CUDTaskResponse>>(response)),
            errors => Problem(errors));
    }
}