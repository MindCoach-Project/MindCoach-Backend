using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Interfaces.Services;
using MinhCoach.App.Common.Persistence;
using MinhCoach.Infra.Authentication;
using MinhCoach.Infra.Persistence;
using MinhCoach.Infra.Persistence.Interceptors;
using MinhCoach.Infra.Persistence.Repositories;
using MinhCoach.Infra.Services;
using MySqlConnector;

namespace MinhCoach.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration)
            .AddPersistance(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(O => O.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });
        return services;
    } 
    
    public static IServiceCollection AddPersistance(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ISubTaskRepository, SubTaskRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<PublishDomainEventsInterceptor>();
        
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<MindCoachDbContext>(options =>
            options.UseMySql(
                connectionString, 
                ServerVersion.AutoDetect(connectionString)));
        
        return services;
    }
}