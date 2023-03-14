using System.Reflection;
using IdentityServer.Entities;
using IdentityServer.Features;
using IdentityServer.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SendGrid;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using Duende.IdentityServer;
using IdentityModel;
using IdentityServer;
using IdentityServer.Abstraction;
using IdentityServer.Services;

var builder = WebApplication.CreateBuilder(args);

//secretClient configuration
var secretClient = new SecretClient(new Uri(builder.Configuration["KeyVaultUri"]!), new DefaultAzureCredential(), new SecretClientOptions()
{
    Retry =
    {
        Delay= TimeSpan.FromSeconds(2),
        MaxDelay = TimeSpan.FromSeconds(16),
        MaxRetries = 5,
        Mode = RetryMode.Exponential
     }
});
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<IdentityServerContext>(options=>
    options.UseSqlServer(((KeyVaultSecret)secretClient.GetSecret("connectionString")).Value));

builder.Services.AddScoped<ISendGridClient>(_ => 
    new SendGridClient(((KeyVaultSecret)secretClient.GetSecret("SendGridKey")).Value));
builder.Services.AddScoped<IEmailSender, SendGridEmailSender>();

builder.Services.AddTransient<IModelStateErrorMessageStore, UkranianModelStateErrorStore>();

builder.Services.AddTransient<ISmsSender>(c=>
    new TwilioSmsSender(((KeyVaultSecret)secretClient.GetSecret("twilioAccountSID")).Value,
    ((KeyVaultSecret)secretClient.GetSecret("twilioAuthToken")).Value));

builder.Services.AddScoped<IPhoneValidator>(c=>
    new PhoneValidator(((KeyVaultSecret)secretClient.GetSecret("abstractApiPhoneValidationKey")).Value));
builder.Services.AddIdentity<User, Role>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireDigit = false;
        options.ClaimsIdentity.EmailClaimType = JwtClaimTypes.Email;
        options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
        options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
        options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
    })
    .AddEntityFrameworkStores<IdentityServerContext>()
    .AddUserManager<UserManager>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>("Default");

builder.Services.AddIdentityConfiguration(((KeyVaultSecret)secretClient.GetSecret("connectionString")).Value);
builder.Services.AddAuthentication().AddGoogle("Google", "Google", opt =>
{
    opt.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
    opt.ClientId = secretClient.GetSecret("GoogleClientId").Value.Value;
    opt.ClientSecret = secretClient.GetSecret("GoogleClientSecret").Value.Value;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});
builder.Services.AddCors(opt=>
{
    opt.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();


app.UseCors("AllowAll");
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