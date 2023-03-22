using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApiAbstraction.Repositories;
using WebApiApplication.Context;
using WebApiInfrastructure.Context;
using WebApiInfrastructure.Repositories;

namespace WebApiInfrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddScoped(typeof(HtmlWeb));
        services.AddScoped<ILatestNewRepository, LatestNewRepository>();
        services.AddDbContext<WebApiDbContext>(options => { options.UseSqlServer(connectionString); });
        services.AddScoped<IWebApiDbContext>(c => c.GetService<WebApiDbContext>()!);
    }
}