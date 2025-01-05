using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Interfaces.Services;
using MinhCoach.Domain.Models;
using MinhCoach.Domain.User;

namespace MinhCoach.Infra.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSetting;
    
    public JwtTokenGenerator(
        IDateTimeProvider dateTimeProvider, 
        IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSetting = jwtOptions.Value;
    }
    
    public string GenerateToken(User user)
    {
        var sigingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSetting.Secret)),
            SecurityAlgorithms.HmacSha256Signature);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        
        var securityToken = new JwtSecurityToken(
            issuer: _jwtSetting.Issuer,
            audience: _jwtSetting.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSetting.ExpiryMinutes),
            claims: claims,
            signingCredentials: sigingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}