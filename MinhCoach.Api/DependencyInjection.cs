using Microsoft.AspNetCore.Mvc.Infrastructure;
using MinhCoach.Api.Common.Errors;
using MinhCoach.Api.Common.Mapping;

namespace MinhCoach.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddHttpContextAccessor();
        
        services.AddSingleton<ProblemDetailsFactory, MinhCoachProblemDetailsFactory>();
        services.AddMappings();

        services.AddPolicyCors();
        
        return services;
    }

    public static IServiceCollection AddPolicyCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                policy => policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        
        return services;
    }
}