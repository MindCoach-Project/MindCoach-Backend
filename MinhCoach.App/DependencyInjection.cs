using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using MinhCoach.App.Common.Behaviors;
using FluentValidation;

namespace MinhCoach.App;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidateBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

}