using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhCoach.App.Common.Response;
using MinhCoach.App.TaskManagement.Commands.CreateTask;
using MinhCoach.App.TaskManagement.Commands.DeleteTask;
using MinhCoach.App.TaskManagement.Commands.UpdateTask;
using MinhCoach.App.TaskManagement.Queries.GetTaskById;
using MinhCoach.App.TaskManagement.Queries.GetTasksByDate;
using MinhCoach.App.TaskManagement.Queries.GetTasksByWeek;
using MinhCoach.App.TaskManagement.Queries.GetUpcomingTasks;
using MinhCoach.Contracts.TaskManagement;

namespace MinhCoach.Api.Controllers;
[Route("task-management/tasks")]
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
        var taskResult = await _mediator.Send(command);
        return taskResult.Match(
            response => Created("url-to-new-resource", _mapper.Map<ApiResponse<CUDTaskResponse>>(response)),
            errors => Problem(errors));
    }
    
    [HttpPut("{taskId}")]
    public async Task<IActionResult> UpdateTask(Guid taskId, UpdateTaskRequest req)
    {
        var command = _mapper.Map<UpdateTaskCommand>((req, taskId));
        var taskResult = await _mediator.Send(command);
        return taskResult.Match(
            response => Ok(_mapper.Map<ApiResponse<CUDTaskResponse>>(response)),
            errors => Problem(errors));
    }
    
    [HttpDelete("{taskId}")]
    public async Task<IActionResult> DeleteTask(Guid taskId)
    {
        var command = _mapper.Map<DeleteTaskCommand>(taskId);
        var taskResult = await _mediator.Send(command);
        return taskResult.Match(
            response => Ok(_mapper.Map<ApiResponse<CUDTaskResponse>>(response)),
            errors => Problem(errors));
    }
    
    [HttpGet("{taskId}")]
    public async Task<IActionResult> GetTask(Guid taskId)
    {
        var query = _mapper.Map<GetTaskByIdQuery>(taskId);
        var taskResult = await _mediator.Send(query);
        return taskResult.Match(
            response => Ok(_mapper.Map<ApiResponse<TaskResponse>>(response)),
            errors=> Problem(errors));
    }
    
    [HttpGet("tasks-by-date")]
    public async Task<IActionResult> GetTasksByDate([FromQuery] DateTime date, [FromQuery] string? status = null)
    {
        var query = _mapper.Map<GetTasksByDateQuery>((date, status));
        var taskResult = await _mediator.Send(query);
        return taskResult.Match(
            response => Ok(_mapper.Map<ApiResponse<List<TaskResponse>>>(response)),
            errors=> Problem(errors));
    }
    
    [HttpGet("tasks-by-week")]
    public async Task<IActionResult> GetTasksByWeek([FromQuery] DateTime? date)
    {
        var query = _mapper.Map<GetTasksByWeekQuery>(date);
        var taskResult = await _mediator.Send(query);
        return taskResult.Match(
            response => Ok(_mapper.Map<ApiResponse<List<UnifiedTaskResponse>>>(response)),
            errors=> Problem(errors));
    }
    
    [HttpGet("upcoming-today")]
    public async Task<IActionResult> GetUpcomingTasksToday()
    {
        var query = new GetUpcomingTasksQuery();
        var taskResult = await _mediator.Send(query);
        return taskResult.Match(
            response => Ok(_mapper.Map<ApiResponse<List<TaskResponse>>>(response)),
            errors=> Problem(errors));
    }
}