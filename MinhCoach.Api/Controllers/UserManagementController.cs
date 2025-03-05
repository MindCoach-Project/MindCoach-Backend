using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MinhCoach.App.Authentication.Commands.UpdateReminderOffset;
using MinhCoach.App.Common.Response;
using MinhCoach.Contracts.UserManagement;

namespace MinhCoach.Api.Controllers;

[Route("users")]
public class UserManagementController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public UserManagementController(
        IMediator mediator, 
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
 
    [HttpPut("update-reminder-offset")]
    public async Task<IActionResult> UpdateReminderOffset(UpdateReminderOffsetRequest req)
    {
        var command = _mapper.Map<UpdateReminderOffsetCommand>(req);
        var userResult = await _mediator.Send(command);
        return userResult.Match(
            response => Ok(_mapper.Map<ApiResponse<ReminderOffsetResponse>>(response)),
            errors => Problem(errors));
    }
}