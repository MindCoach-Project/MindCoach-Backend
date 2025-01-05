using Microsoft.AspNetCore.Mvc.Infrastructure;
using MinhCoach.Api.Common.Errors;
using MinhCoach.Api.Common.Mapping;

namespace MinhCoach.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, MinhCoachProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }

}