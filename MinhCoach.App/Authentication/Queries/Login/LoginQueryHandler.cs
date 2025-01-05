using ErrorOr;
using MediatR;
using MinhCoach.App.Authentication.Common;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Persistence;
using MinhCoach.App.Common.Errors;
using MinhCoach.Domain.Models;
using MinhCoach.Domain.User;

namespace MinhCoach.App.Authentication.Queries.Login;

public class LoginQueryHandler :
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<AuthenticationResult>> Handle(
        LoginQuery query, 
        CancellationToken cancellationToken)
    {
        //validate the user exists
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
            return Errors.Authentication.InvalidCredentials;
        
        //validate the password is correct
        if (user.Password != query.Password)
            return Errors.Authentication.InvalidCredentials;
        
        //create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token);
    }
}