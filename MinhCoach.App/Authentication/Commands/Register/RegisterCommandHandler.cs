using MediatR;
using ErrorOr;
using MinhCoach.App.Authentication.Common;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Persistence;
using MinhCoach.App.Common.Response;
using MinhCoach.Domain.Errors;
using MinhCoach.Domain.User;

namespace MinhCoach.App.Authentication.Commands.Register;

public class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<ObjectResponse<AuthenticationResult>>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator, 
        IPasswordHasher passwordHasher,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<ObjectResponse<AuthenticationResult>>> Handle(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {   
        //check if user already exists
        if (_userRepository.GetUserByEmail(command.Email) is not null)
            return Errors.User.DuplicateEmail;
        
        //hash password
        string hashedPassword  = _passwordHasher.HashPassword(command.Password); 
        
        //create user
        var user = User.Create(
            command.Username,
            command.Email, 
            hashedPassword
            );

        _userRepository.Add(user);
        
        //create jwt token
        string token = _jwtTokenGenerator.GenerateToken(user);
        
        return new ObjectResponse<AuthenticationResult>(
            "Registration successful, please confirm your email.", 
            new AuthenticationResult(
                user,
                token)
        );
    }

}