using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Interfaces.Services;
using MinhCoach.App.Common.Persistence;
using MinhCoach.Infra.Authentication;
using MinhCoach.Infra.Persistence;
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

        return services;
    }
    
    public static IServiceCollection AddPersistance(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<MindCoachDbContext>(options =>
            options.UseMySql(
                connectionString, 
                ServerVersion.AutoDetect(connectionString)));
        
        return services;
    }
}