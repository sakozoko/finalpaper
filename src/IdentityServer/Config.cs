using IdentityServer4.Models;

namespace IdentityServer;

public class Config
{
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