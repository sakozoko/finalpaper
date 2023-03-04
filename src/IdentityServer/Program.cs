using IdentityServer;
using IdentityServer.Entities;
using IdentityServer.Features;
using IdentityServer.Persistence;
using IdentityServer4;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SendGrid;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<IdentityServerContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ISendGridClient>(_ => new SendGridClient(builder.Configuration["SendGridKey"]));

builder.Services.AddIdentity<User, Role>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireDigit = false;
    })
    .AddEntityFrameworkStores<IdentityServerContext>()
    .AddUserManager<UserManager>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>("Default");

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<User>()
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddDeveloperSigningCredential();
builder.Services.AddAuthentication().AddGoogle("Google", "Google", opt =>
{
    opt.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
    opt.ClientId = builder.Configuration["Google.ClientId"]??string.Empty;
    opt.ClientSecret = builder.Configuration["Google.ClientSecret"]??string.Empty;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

await app.SeedIdentityModels();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.UseIdentityServer();



app.Run();