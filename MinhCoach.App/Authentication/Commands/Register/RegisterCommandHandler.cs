// using MediatR;
// using ErrorOr;
// using MinhCoach.App.Authentication.Common;
// using MinhCoach.App.Common.Interfaces.Authentication;
// using MinhCoach.App.Common.Persistence;
// using MinhCoach.Domain.Errors;
// using MinhCoach.Domain.Models;
// using MinhCoach.Domain.User;
//
// namespace MinhCoach.App.Authentication.Commands.Register;
//
// public class RegisterCommandHandler :
//     IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
// {
//     private readonly IJwtTokenGenerator _jwtTokenGenerator;
//     private readonly IUserRepository _userRepository;
//     public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
//     {
//         _jwtTokenGenerator = jwtTokenGenerator;
//         _userRepository = userRepository;
//     }
//     
//     public async Task<ErrorOr<AuthenticationResult>> Handle(
//         RegisterCommand command,
//         CancellationToken cancellationToken)
//     {   
//         //check if user already exists
//         if (_userRepository.GetUserByEmail(command.Email) is not null)
//             return Errors.User.DuplicateEmail;
//
//         //create user
//         var user = User.Create(
//             command.Email, 
//             command.Password,
//             command.LastName,
//             command.FirstName);
//         
//         _userRepository.Add(user);
//         
//         //create jwt token
//         string token = _jwtTokenGenerator.GenerateToken(user);
//         
//         return new AuthenticationResult(
//             user,
//             token);
//     }
//
// }