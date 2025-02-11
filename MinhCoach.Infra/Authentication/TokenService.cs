using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.Infra.Common.Errors;

namespace MinhCoach.Infra.Authentication;

public class TokenService : ITokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public Guid GetUserIdFromToken()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        
        //get token
        var token = httpContext?.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
        
        if (string.IsNullOrEmpty(token))
        {
            return Guid.Empty;
        }
        
        //decode token
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtTokenHandler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken == null)
        {
            return Guid.Empty;
        }
        
        //get userIdClaim
        var userIdClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName);

        if (userIdClaim == null)
        {
            return Guid.Empty;
        }
        
        //get userId
        if (!Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Guid.Empty;
        }

        return userId;
    }
}