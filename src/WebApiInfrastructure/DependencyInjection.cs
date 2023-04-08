using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApiApplication.Context;
using WebApiApplication.Repositories;
using WebApiInfrastructure.Context;
using WebApiInfrastructure.Repositories;

namespace WebApiInfrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddScoped(typeof(HtmlWeb));
        services.AddScoped<ILatestNewRepository, LatestNewRepository>();
        services.AddScoped<IVolunteerOrganizationRepository, VolunteerOrganizationRepository>();
        services.AddDbContext<WebApiDbContext>(options => { options.UseSqlServer(connectionString); });
        services.AddScoped<IWebApiDbContext>(c => c.GetService<WebApiDbContext>()!);
    }
}