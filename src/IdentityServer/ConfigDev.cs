using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using IdentityModel;
using IdentityServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer;

public static class ConfigDev
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource("roles", "Your role(s)", new[] { "role" }),
            new IdentityResource("phoneNumberVerified", "Your phone number confirmation status",
                new[] { JwtClaimTypes.PhoneNumberVerified }),
            new IdentityResource("emailVerified", "Your email confirmation status",
                new[] { JwtClaimTypes.EmailVerified })
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            new Client
            {
                ClientId = "webapi",
                ClientName = "Web API",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "webapi" }
            },
            new Client
            {
                ClientId = "webapp",
                ClientName = "Web App",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = false,
                RequireClientSecret = false,
                RedirectUris = { "https://localhost:5002/sign-in-oidc-callback" },
                PostLogoutRedirectUris = { "https://localhost:5002/sign-out-oidc-callback" },
                AllowedCorsOrigins = { "https://localhost:5002" },
                AllowedScopes =
                    { "openid", "profile", "email", "roles", "phoneNumberVerified", "emailVerified", "webapi" },
                AllowOfflineAccess = true
            }
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new[]
        {
            new ApiResource("webapi", "Web API")
            {
                Scopes = { "webapi" },
                UserClaims = { "role", JwtClaimTypes.PhoneNumberVerified, JwtClaimTypes.EmailVerified }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("webapi", "Web API")
        };

    public static IServiceCollection AddIdentityConfigurationA(this IServiceCollection services,
        string connectionString)
    {
        services.AddIdentityServer()
            .AddInMemoryClients(Clients)
            .AddInMemoryApiResources(ApiResources)
            .AddInMemoryApiScopes(ApiScopes)
            .AddInMemoryIdentityResources(IdentityResources)
            .AddDeveloperSigningCredential()
            .AddAspNetIdentity<User>();
        return services;
    }

    public static void InitializeDatabase(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
        {
            serviceScope?.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope?.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            if (context is null) return;
            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                foreach (var client in Clients) context.Clients.Add(client.ToEntity());
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in IdentityResources) context.IdentityResources.Add(resource.ToEntity());
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in ApiScopes) context.ApiScopes.Add(resource.ToEntity());
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in ApiResources) context.ApiResources.Add(resource.ToEntity());
                context.SaveChanges();
            }
        }
    }
}