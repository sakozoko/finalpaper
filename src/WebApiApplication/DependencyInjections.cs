using Microsoft.Extensions.DependencyInjection;
using WebApiApplication.Services;

namespace WebApiApplication;

public static class DependencyInjections
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ILatestNewService, LatestNewService>();
        return services;
    }
}