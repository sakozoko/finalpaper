using System.Reflection;
using WebApiApplication.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using WebApiApplication.Validation;

namespace WebApiApplication;

public static class DependencyInjections
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ILatestNewService, LatestNewService>();
        services.AddMediatR(c=> c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
} 