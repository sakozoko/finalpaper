using IdentityServer.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Persistence;

public static class DatabaseSeederExtension
{
    public static async Task<IApplicationBuilder> SeedIdentityModels(this IApplicationBuilder app)
    {
        await using var scope = app.ApplicationServices.CreateAsyncScope();
        try
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            await roleManager.CreateAsync(new() { Name = "Admin" });
            await roleManager.CreateAsync(new() { Name = "User" });
            var user = new User
            {
                UserName = "admin",
                Email = "admin@localhost",
                EmailConfirmed = true,
                PhoneNumber = "123456789",
                PhoneNumberConfirmed = true,
                SecurityStamp = new Guid().ToString()
            };
            var result = await userManager.CreateAsync(user, "Password1");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        }

        return app;
    }
}