using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhCoach.App.Common.Response;
using MinhCoach.App.TemplateManagement.Queries.GetTemplates;
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
    public async Task<IActionResult> GetSystemTemplates()
    {
        var query = new GetTemplatesQuery();
        var taskResult = await _mediator.Send(query);
        return taskResult.Match(
            response =>
            {
                Console.WriteLine(response);
                return Ok(_mapper.Map<ApiResponse<List<TemplateResponse>>>(response));
            },
            errors=> Problem(errors));
    }
}