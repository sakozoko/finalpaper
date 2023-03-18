using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using WebApiApplication;
using WebApiInfrastructure;


var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.
builder.Services.AddInfrastructure(((KeyVaultSecret)secretClient.GetSecret("connectionString")).Value);
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddAuthentication().AddJwtBearer("Bearer", opt =>
{
    opt.Authority = "https://identityserverfinalwork.azurewebsites.net";
    opt.Audience = "webapi";
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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}
app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();