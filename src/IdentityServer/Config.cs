using System.Reflection;
using IdentityServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer;

public static class Config
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, string connectionString)
    {
        services.AddIdentityServer()
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext =
                    b=>b.UseSqlServer(connectionString,
                        sql=>sql.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name));
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext =
                    b => b.UseSqlServer(connectionString,
                        sql=>sql.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name));
            })
            .AddAspNetIdentity<User>()
            .AddDeveloperSigningCredential();
        return services;
    }
}