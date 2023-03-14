using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer;

public class Config
{
    public static void InitializeDatabase(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes)
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            if(!context.ApiResources.Any())
            {
                foreach (var resource in Config.ApiResources)
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
    public static IEnumerable<IdentityResource> IdentityResources =>
    new []{
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
        new IdentityResource("roles", "Your role(s)", new []{"role"}),
    };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            new Client()
            {
                ClientId = "webapi",
                ClientName = "Web API",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "webapi" },
            },
            new Client()
            {
                ClientId = "webapp",
                ClientName = "Web App",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = false,
                RequireClientSecret = false,
                RedirectUris = { "https://clientfinalwork.azurewebsites.net" },
                PostLogoutRedirectUris = { "https://clientfinalwork.azurewebsites.net" },
                AllowedCorsOrigins = { "https://clientfinalwork.azurewebsites.net" },
                AllowedScopes = { "openid", "profile", "email", "roles", "webapi" },
                AllowOfflineAccess = true,
            },
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new[]
        {
            new ApiResource("webapi", "Web API")
            {
                Scopes = { "webapi" },
                UserClaims = { "role" },
            },

        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("webapi", "Web API"),
        };
}