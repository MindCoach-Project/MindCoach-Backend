// using MapsterMapper;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Authorization;
// using MinhCoach.Contracts.Authentication;
// using MediatR;
// using ErrorOr;
// using MinhCoach.App.Authentication.Commands.Register;
// using MinhCoach.App.Authentication.Common;
// using MinhCoach.App.Authentication.Queries.Login;
//
// namespace MinhCoach.Api.Controllers;
//
// [Route("auth")]
// [AllowAnonymous]
// public class AuthenticationController : ApiController
// {
//     private readonly ISender _mediator;
//     private readonly IMapper _mapper;
//
//     public AuthenticationController(IMediator mediator, IMapper mapper)
//     {
//         _mediator = mediator;
//         _mapper = mapper;
//     }
//     //
//     // [HttpPost("register")]
//     // public async Task<IActionResult> Register(RegisterRequest req)
//     // {
//     //     var command = _mapper.Map<RegisterCommand>(req);
//     //     ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);
//     //     return authResult.Match(
//     //         authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
//     //         errors => Problem(errors));
//     // }
//     // [HttpPost("login")]
//     // public async Task<IActionResult> Login(LoginRequest req)
//     // {
//     //     var query = _mapper.Map<LoginQuery>(req);
//     //     ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);
//     //     return authResult.Match(
//     //         authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
//     //         errors => Problem(errors));
//     // }
// }