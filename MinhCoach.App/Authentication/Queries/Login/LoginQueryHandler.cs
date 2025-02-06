using ErrorOr;
using MediatR;

using MinhCoach.App.Authentication.Common;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Persistence;
using MinhCoach.App.Common.Errors;
using MinhCoach.App.Common.Response;
using MinhCoach.Domain.User;

namespace MinhCoach.App.Authentication.Queries.Login;

public class LoginQueryHandler :
    IRequestHandler<LoginQuery, ErrorOr<ObjectResponse<AuthenticationResult>>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator, 
        IPasswordHasher passwordHasher,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<ObjectResponse<AuthenticationResult>>> Handle(
        LoginQuery query, 
        CancellationToken cancellationToken)
    {
        //validate the user exists
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
            return Errors.Authentication.InvalidCredentials;

        //validate the password is correct
        if (!_passwordHasher.VerifyPassword(query.Password, user.Password))
            return Errors.Authentication.InvalidCredentials;
        
        //create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new ObjectResponse<AuthenticationResult>(
            "Login successful, welcome back!", 
             new AuthenticationResult(
                user,
                token)
            );
    }
}