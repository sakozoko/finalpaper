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
            new IdentityResource("phoneNumber", "Your phone number",
                new[] { JwtClaimTypes.PhoneNumber, JwtClaimTypes.PhoneNumberVerified}),
            new IdentityResource("username", "Your username", new[] { "username" })
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
                RedirectUris = { "https://clientfinalwork.azurewebsites.net/sign-in-oidc-callback" },
                PostLogoutRedirectUris = { "https://clientfinalwork.azurewebsites.net/sign-out-oidc-callback" },
                AllowedCorsOrigins = { "https://clientfinalwork.azurewebsites.net" },
                AllowedScopes =
                    { "openid", "profile", "email", "roles", "webapi", "phoneNumber" },
                AllowOfflineAccess = true
            }
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new[]
        {
            new ApiResource("webapi", "Web API")
            {
                Scopes = { "webapi" },
                UserClaims = { "role", JwtClaimTypes.PhoneNumberVerified, JwtClaimTypes.EmailVerified, JwtClaimTypes.PhoneNumber, JwtClaimTypes.Email, JwtClaimTypes.Role, JwtClaimTypes.Name, JwtClaimTypes.GivenName, JwtClaimTypes.FamilyName }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("webapi", "Web API")
        };

    public static IServiceCollection AddIdentityConfigurationA(this IServiceCollection services)
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
            var persistedContext = serviceScope?.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
            persistedContext?.Database.Migrate();


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